using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Patient.Queries;

public record GetPatientDetailsQuery(int Id):IRequest<PatientViewModel>;