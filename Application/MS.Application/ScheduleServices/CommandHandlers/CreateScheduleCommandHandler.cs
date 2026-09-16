using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Schedule.Commands;
using MS.Domain.Entities.Schedules;

namespace MS.Application.ScheduleServices.CommandHandlers;

public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, OperationResult>
{
    private readonly IScheduleRepository _scheduleRepository;

    public CreateScheduleCommandHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<OperationResult> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        OperationResult operation = new();

        try
        {
            if (request is null)
            {
                return operation.Failed(ApplicationMessages.ValueMustNotBeNull);
            }

            if (await _scheduleRepository.ExistAsync(x => x.DoctorId == request.DoctorId && x.Date == request.Date))
            {
                return operation.Failed(ApplicationMessages.ValueCannotBeDuplicated);
            }

            var schedule = new Schedule(request.DoctorId, request.Date, request.WorkStart, request.WorkEnd);
            await _scheduleRepository.CreateAsync(schedule);
            await _scheduleRepository.SaveChangesAsync();
            return operation.Succeeded();
        }
        catch (Exception e)
        {
            return operation.Failed(e.Message);
        }
        
    }
}

