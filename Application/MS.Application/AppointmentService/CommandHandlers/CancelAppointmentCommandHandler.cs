using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Appointment.Commands;
using MS.Domain.Entities.Appointments;

namespace MS.Application.AppointmentService.CommandHandlers;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, OperationResult>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<OperationResult> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        OperationResult operation = new();
        try
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            appointment.Cancel();
            await _appointmentRepository.SaveChangesAsync();
            return operation.Succeeded();
        }
        catch (Exception e)
        {
            return operation.Failed(e.Message);
        }
    }
}

