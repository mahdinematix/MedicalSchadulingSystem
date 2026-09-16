using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Doctor.Queries;
using MS.Domain.Entities.Doctors;

namespace MS.Application.DoctorService.QueryHandlers;

public class GetDoctorDetailsByIdQueryHandler : IRequestHandler<GetDoctorDetailsByIdQuery, DoctorViewModel>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorDetailsByIdQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DoctorViewModel> Handle(GetDoctorDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id);

        if (doctor == null)
        {
            return null;
        }

        return new DoctorViewModel
        {
            Id = doctor.Id,
            Fullname = doctor.Fullname,
            MedicalSystemNumber = doctor.MedicalSystemNumber,
            Speciality = doctor.Speciality
        };
    }
}