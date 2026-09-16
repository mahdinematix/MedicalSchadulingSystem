using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Doctor.Queries
{
    public record GetDoctorDetailsByIdQuery(int Id) : IRequest<DoctorViewModel>
    {
    }
}
