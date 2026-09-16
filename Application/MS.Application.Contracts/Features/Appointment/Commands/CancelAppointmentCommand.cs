using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Appointment.Commands;

public record CancelAppointmentCommand(int AppointmentId) : IRequest<OperationResult>;

