using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Patient.Queries;
using MS.Domain.Entities.Patients;

namespace MS.Application.PatientServices.QueryHandlers;

public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, List<PatientViewModel>>
{
    private readonly IPatientRepository _patientRepository;

    public GetAllPatientsQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<List<PatientViewModel>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await _patientRepository.GetAllAsync();
        if (patients == null || !patients.Any())
        {
            return new List<PatientViewModel>();
        }
        return patients.Select(x=> new PatientViewModel
        {
            Id = x.Id,
            Fullname = x.Fullname,
            Mobile = x.Mobile
        }).ToList();
    }
}