using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Doctor.Commands;
using MS.Domain.Entities.Doctors;

namespace MS.Application.DoctorService.CommandHandlers;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, OperationResult>
{
    private readonly IDoctorRepository _doctorRepository;

    public CreateDoctorCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<OperationResult> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        OperationResult operation = new OperationResult();
        try
        {
            if (request is null)
            {
                return operation.Failed(ApplicationMessages.ValueMustNotBeNull);
            }

            if (await _doctorRepository.ExistAsync(x=>x.Fullname.ToLower() == request.Fullname.Trim().ToLower()))
            {
                return operation.Failed(ApplicationMessages.ValueCannotBeDuplicated);
            }

            Doctor doctor = new Doctor(request.Fullname, request.Speciality, request.MedicalSystemNumber);
            await _doctorRepository.CreateAsync(doctor);
            await _doctorRepository.SaveChangesAsync();
            return operation.Succeeded();

        }
        catch (Exception e)
        {
            return operation.Failed(e.Message);
        }
    }
}
