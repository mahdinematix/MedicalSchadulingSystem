using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Schedule.Commands;

public record UpdateScheduleCommand(int Id, int DoctorId, DateOnly Date, TimeOnly WorkStart, TimeOnly WorkEnd)
    : IRequest<OperationResult>;