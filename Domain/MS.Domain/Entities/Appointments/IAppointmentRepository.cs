using MS.Domain.Common;

namespace MS.Domain.Entities.Appointments;

public interface IAppointmentRepository : IGenericRepository<int, Appointment>
{
    Task<bool> IsOverLapping(int doctorId, DateOnly date, TimeOnly time);
    Task<List<Appointment>> GetAppointmentsByPatientId(int patientId);
    Task<bool> IsReserved(int doctorId, DateOnly date, TimeOnly time);
}
