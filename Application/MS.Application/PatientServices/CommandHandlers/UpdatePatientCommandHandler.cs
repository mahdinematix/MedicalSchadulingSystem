using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Patient.Commands;
using MS.Domain.Entities.Patients;

namespace MS.Application.PatientServices.CommandHandlers;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, OperationResult>
{
    private readonly IPatientRepository _patientRepository;

    public UpdatePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<OperationResult> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        OperationResult operation = new();
        try
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id);
            if (patient == null)
            {
                return operation.Failed(ApplicationMessages.ValueMustNotBeNull);
            }

            if (await _patientRepository.ExistAsync(x => x.Fullname.ToLower() == request.Fullname.Trim().ToLower()))
            {
                return operation.Failed(ApplicationMessages.ValueCannotBeDuplicated);
            }

            patient.Update(request.Fullname.Trim(), request.Mobile.Trim());

            await _patientRepository.SaveChangesAsync();
            return operation.Succeeded();
        }
        catch (Exception e)
        {
            return operation.Failed(e.Message);
        }

    }
}