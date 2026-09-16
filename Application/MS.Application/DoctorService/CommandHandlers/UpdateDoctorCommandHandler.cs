using MediatR;
using MS.Application.Contracts.Common;
using MS.Application.Contracts.Features.Doctor.Commands;
using MS.Domain.Entities.Doctors;

namespace MS.Application.DoctorService.CommandHandlers;

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, OperationResult>
{
    private readonly IDoctorRepository _doctorRepository;

    public UpdateDoctorCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<OperationResult> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        OperationResult operation = new OperationResult();
        try
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.Id);
            if (doctor == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (await _doctorRepository.ExistAsync(x => x.Fullname.ToLower() == request.Fullname.Trim().ToLower()))
            {
                return operation.Failed(ApplicationMessages.ValueCannotBeDuplicated);
            }
            doctor.Update(request.Fullname, request.Speciality, request.MedicalSystemNumber);

            _doctorRepository.Update(doctor);
            await _doctorRepository.SaveChangesAsync();
            return operation.Succeeded();

        }
        catch (Exception e)
        {
            return operation.Failed(e.Message);
        }
    }
}
