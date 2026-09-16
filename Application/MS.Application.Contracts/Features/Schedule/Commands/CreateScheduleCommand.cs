using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Schedule.Commands;

public class CreateScheduleCommand:IRequest<OperationResult>
{
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly WorkStart { get; set; }
    public TimeOnly WorkEnd { get; set; }
}