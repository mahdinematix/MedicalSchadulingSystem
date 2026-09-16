using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities.Schedules;

namespace MS.Infrastructure.EFCore.Repositories;

public class ScheduleRepository : GenericRepository<int, Schedule>, IScheduleRepository
{
    private readonly ApplicationContext _context;
    public ScheduleRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Schedule>> GetSchedulesByDoctorIdAsync(int doctorId)
    {
        return await _context.Schedules.Where(x => x.DoctorId == doctorId && x.Date >= DateOnly.FromDateTime(DateTime.Today)).Include(x=>x.Doctor).OrderByDescending(x=>x.Date).AsNoTracking().ToListAsync();
    }

    public async Task<(TimeOnly WorkStart, TimeOnly WorkEnd)?> GetDoctorWorkingHoursForSpecificDate(int doctorId, DateOnly date)
    {
        var schedule = await _context.Schedules.FirstOrDefaultAsync(x => x.DoctorId == doctorId && x.Date == date);
        return (schedule.WorkStart,schedule.WorkEnd);
    }

    public async Task<bool> IsWorkingHoursDefinedForSpecificDateAndTimeAsync(int doctorId, DateOnly date, TimeOnly time)
    {
        return await _context.Schedules.AnyAsync(x =>
            x.DoctorId == doctorId && x.Date == date && x.WorkStart <= time && x.WorkEnd > time);
    }
}