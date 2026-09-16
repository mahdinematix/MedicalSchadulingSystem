using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Patient.Queries;
using MS.Domain.Entities.Patients;

namespace MS.Application.PatientServices.QueryHandlers;

public class GetPatientDetailsQueryHandler : IRequestHandler<GetPatientDetailsQuery, PatientViewModel>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientDetailsQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientViewModel> Handle(GetPatientDetailsQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id);
        if (patient == null) return null;

        return new PatientViewModel
        {
            Id = patient.Id,
            Fullname = patient.Fullname,
            Mobile = patient.Mobile
        }
            ;
    }
}