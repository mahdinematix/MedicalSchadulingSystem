using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Schedule.Queries;
using MS.Domain.Entities.Appointments;
using MS.Domain.Entities.Schedules;

namespace MS.Application.ScheduleServices.QueryHandlers;

public class GetDoctorWorkingHoursForSpecificDateQueryHandler : IRequestHandler<GetDoctorWorkingHoursForSpecificDateQuery, List<AppointmentTime>>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public GetDoctorWorkingHoursForSpecificDateQueryHandler(IScheduleRepository scheduleRepository, IAppointmentRepository appointmentRepository)
    {
        _scheduleRepository = scheduleRepository;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<List<AppointmentTime>> Handle(GetDoctorWorkingHoursForSpecificDateQuery request, CancellationToken cancellationToken)
    {
        var workingHours = await _scheduleRepository.GetDoctorWorkingHoursForSpecificDate(request.DoctorId, request.Date);
        if (workingHours == null)
        {
            return new List<AppointmentTime>();
        }

        var times = new List<AppointmentTime>();
        var current = workingHours.Value.WorkStart;
        while (current < workingHours.Value.WorkEnd)
        {
            var time = new AppointmentTime
            {
                Time = current,
                IsReserved = await _appointmentRepository.IsReserved(request.DoctorId,request.Date,current)
            };
            times.Add(time);
            current = current.AddMinutes(20);
        }

        return times;
    }
}

