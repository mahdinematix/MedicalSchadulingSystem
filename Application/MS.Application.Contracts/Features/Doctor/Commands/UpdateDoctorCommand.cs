using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Doctor.Commands;

public class UpdateDoctorCommand : IRequest<OperationResult>
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Speciality { get; set; }
    public string MedicalSystemNumber { get; set; }
}
