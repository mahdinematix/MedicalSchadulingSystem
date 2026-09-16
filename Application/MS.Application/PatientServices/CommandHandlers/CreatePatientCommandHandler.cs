using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Patient.Commands;
using MS.Domain.Entities.Patients;

namespace MS.Application.PatientServices.CommandHandlers;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, OperationResult>
{
    private readonly IPatientRepository _patientRepository;

    public CreatePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<OperationResult> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        OperationResult operation = new();
        try
        {
            if (request == null)
            {
                return operation.Failed(ApplicationMessages.ValueMustNotBeNull);
            }

            if (await _patientRepository.ExistAsync(x => x.Fullname.ToLower() == request.Fullname.Trim().ToLower()))
            {
                return operation.Failed(ApplicationMessages.ValueCannotBeDuplicated);
            }

            var patient = new Patient(request.Fullname.Trim(), request.Mobile.Trim());

            await _patientRepository.CreateAsync(patient);
            await _patientRepository.SaveChangesAsync();
            return operation.Succeeded();
        }
        catch (Exception e)
        {
            return operation.Failed(e.Message);
        }
       
    }
}