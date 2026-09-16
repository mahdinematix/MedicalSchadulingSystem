using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Doctor.Queries;

public record GetAllDoctorsQuery() : IRequest<List<DoctorViewModel>>;