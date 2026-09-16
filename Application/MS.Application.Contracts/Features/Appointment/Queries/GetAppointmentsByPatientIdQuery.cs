using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Appointment.Queries;

public record GetAppointmentsByPatientIdQuery(int PatientId) : IRequest<List<AppointmentViewModel>>;