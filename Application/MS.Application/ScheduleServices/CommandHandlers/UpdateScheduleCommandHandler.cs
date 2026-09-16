using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Schedule.Commands;
using MS.Domain.Entities.Schedules;

namespace MS.Application.ScheduleServices.CommandHandlers;

public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, OperationResult>
{
    private readonly IScheduleRepository _scheduleRepository;

    public UpdateScheduleCommandHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<OperationResult> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        OperationResult operation = new();
        try
        {
            var schedule = await _scheduleRepository.GetByIdAsync(request.Id);
            if (schedule is null)
            {
                return operation.Failed(ApplicationMessages.ValueMustNotBeNull);
            }

            if (await _scheduleRepository.ExistAsync(x => x.DoctorId == request.DoctorId && x.Date == request.Date && x.Id != request.Id))
            {
                return operation.Failed(ApplicationMessages.ValueCannotBeDuplicated);
            }

            schedule.Update(request.DoctorId, request.Date, request.WorkStart, request.WorkEnd);
            _scheduleRepository.Update(schedule);
            await _scheduleRepository.SaveChangesAsync();
            return operation.Succeeded();
        }
        catch (Exception e)
        {
            return operation.Failed(e.Message);
        }
    }
}

