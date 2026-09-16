using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Patient.Commands;

public record CreatePatientCommand(string Fullname, string Mobile) : IRequest<OperationResult>;