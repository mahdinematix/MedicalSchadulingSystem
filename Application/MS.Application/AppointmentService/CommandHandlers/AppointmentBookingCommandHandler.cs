using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Appointment.Commands;
using MS.Domain.Entities.Appointments;
using MS.Domain.Entities.Patients;
using MS.Domain.Entities.Schedules;

namespace MS.Application.AppointmentService.CommandHandlers;

public class AppointmentBookingCommandHandler : IRequestHandler<AppointmentBookingCommand, OperationResult>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public AppointmentBookingCommandHandler(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IScheduleRepository scheduleRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<OperationResult> Handle(AppointmentBookingCommand request, CancellationToken cancellationToken)
    {
        OperationResult operation = new();
        try
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
            {
                return operation.Failed(ApplicationMessages.ThereIsNoPatientWithThisId);
            }

            var isWorkingHoursDefined =
                await _scheduleRepository.IsWorkingHoursDefinedForSpecificDateAndTimeAsync(request.DoctorId,
                    request.Date, request.Time);

            if (!isWorkingHoursDefined)
            {
                return operation.Failed(ApplicationMessages.NoWorkingHorsDefinedForSpecificDateAndTime);
            }

            var isOverLapping = await _appointmentRepository.IsOverLapping(request.DoctorId,
                request.Date, request.Time);
            if (isOverLapping)
            {
                return operation.Failed(ApplicationMessages.ThisTimeIsReserved);
            }

            var appointment = new Appointment(request.PatientId, request.DoctorId, request.Date, request.Time);
            await _appointmentRepository.CreateAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();
            return operation.Succeeded();
        }
        catch (Exception e)
        {
            return operation.Failed(e.Message);
        }
        
    }
}

