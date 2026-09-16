using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Schedule.Queries;

public record GetSchedulesByDoctorIdQuery(int DoctorId) : IRequest<List<ScheduleViewModel>>;

