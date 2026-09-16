using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Appointment.Queries;
using MS.Domain.Entities.Appointments;

namespace MS.Application.AppointmentService.QueryHandlers;

public class GetAppointmentsByPatientIdQueryHandler : IRequestHandler<GetAppointmentsByPatientIdQuery, List<AppointmentViewModel>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentsByPatientIdQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<List<AppointmentViewModel>> Handle(GetAppointmentsByPatientIdQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAppointmentsByPatientId(request.PatientId);
        if (appointments == null || !appointments.Any())
        {
            return new List<AppointmentViewModel>();
        }

        return appointments.Select(x => new AppointmentViewModel
        {
            PatientId = x.PatientId,
            Date = x.Date,
            Time = x.Time,
            DoctorId = x.DoctorId,
            DoctorName = x.Doctor.Fullname,
            PatientName = x.Patient.Fullname,
            Status = x.Status.ToString()
        }).ToList();
    }
}

