using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Appointment.Commands;
using MS.Domain.Entities.Appointments;
using MS.Domain.Entities.Schedules;

namespace MS.Application.AppointmentService.CommandHandlers;

public class ChangeAppointmentCommandHandler : IRequestHandler<ChangeAppointmentCommand, OperationResult>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public ChangeAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IScheduleRepository scheduleRepository)
    {
        _appointmentRepository = appointmentRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<OperationResult> Handle(ChangeAppointmentCommand request, CancellationToken cancellationToken)
    {
        OperationResult operation = new();
        try
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
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

            appointment.ReSchedule(request.Date,request.Time);
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();
            return operation.Succeeded();
        }
        catch (Exception e)
        {
            return operation.Failed(e.Message);
        }
        
    }
}

