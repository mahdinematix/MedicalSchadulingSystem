using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Appointment.Commands;

public class AppointmentBookingCommand:IRequest<OperationResult>
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}

