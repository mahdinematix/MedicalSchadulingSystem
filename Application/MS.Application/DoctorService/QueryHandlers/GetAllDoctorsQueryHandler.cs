using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Doctor.Queries;
using MS.Domain.Entities.Doctors;

namespace MS.Application.DoctorService.QueryHandlers;

public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, List<DoctorViewModel>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }


    public async Task<List<DoctorViewModel>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _doctorRepository.GetAllAsync();
        if (doctors == null || !doctors.Any())
        {
            return new List<DoctorViewModel>();
        }

        return doctors.Select(x => new DoctorViewModel
        {
            Id = x.Id,
            Fullname = x.Fullname,
            MedicalSystemNumber = x.MedicalSystemNumber,
            Speciality = x.Speciality
        }).ToList();
    }
}