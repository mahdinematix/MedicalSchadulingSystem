using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities.Appointments;

namespace MS.Infrastructure.EFCore.Repositories;

public class AppointmentRepository : GenericRepository<int, Appointment>, IAppointmentRepository
{
    private readonly ApplicationContext _context;
    public AppointmentRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsOverLapping(int doctorId, DateOnly date, TimeOnly time)
    {
        return await _context.Appointments.AnyAsync(x => x.DoctorId == doctorId && x.Date == date && x.Time == time && x.Status == AppointmentStatus.Reserved);
    }

    public async Task<List<Appointment>> GetAppointmentsByPatientId(int patientId)
    {
        return await _context.Appointments.Where(x => x.PatientId == patientId && x.Date >= DateOnly.FromDateTime(DateTime.Today)).Include(x=>x.Doctor).Include(x=>x.Patient).OrderByDescending(x => x.Date)
            .AsNoTracking().ToListAsync();
    }

    public async Task<bool> IsReserved(int doctorId, DateOnly date, TimeOnly time)
    {
        return await _context.Appointments.AnyAsync(x => x.DoctorId == doctorId && x.Date == date && x.Time == time);
    }
}