using MediatR;
using MS.Application.Contracts.Common;

namespace MS.Application.Contracts.Features.Schedule.Queries;

public class GetDoctorWorkingHoursForSpecificDateQuery : IRequest<List<AppointmentTime>>
{
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
}