using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Schedule.Queries;
using MS.Domain.Entities.Schedules;

namespace MS.Application.ScheduleServices.QueryHandlers;

public class GetSchedulesByDoctorIdQueryHandler : IRequestHandler<GetSchedulesByDoctorIdQuery, List<ScheduleViewModel>>
{
    private readonly IScheduleRepository _scheduleRepository;

    public GetSchedulesByDoctorIdQueryHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<List<ScheduleViewModel>> Handle(GetSchedulesByDoctorIdQuery request, CancellationToken cancellationToken)
    {
        var schedules = await _scheduleRepository.GetSchedulesByDoctorIdAsync(request.DoctorId);

        if (schedules == null || !schedules.Any())
        {
            return new List<ScheduleViewModel>();
        }

        return schedules.Select(x => new ScheduleViewModel
        {
            DoctorId = x.DoctorId,
            WorkStart = x.WorkStart,
            WorkEnd = x.WorkEnd,
            Date = x.Date,
            DoctorName = x.Doctor.Fullname,
            Id = x.Id
        }).ToList();

    }
}
