using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Appointment.Commands;

public class ChangeAppointmentCommand : IRequest<OperationResult>
{
    public int AppointmentId { get; set; }
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}