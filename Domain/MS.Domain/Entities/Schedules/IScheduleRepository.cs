using MS.Domain.Common;

namespace MS.Domain.Entities.Schedules;

public interface IScheduleRepository : IGenericRepository<int, Schedule>
{
    Task<List<Schedule>> GetSchedulesByDoctorIdAsync(int doctorId);
    Task<(TimeOnly WorkStart, TimeOnly WorkEnd)?> GetDoctorWorkingHoursForSpecificDate(int doctorId, DateOnly date);
    Task<bool> IsWorkingHoursDefinedForSpecificDateAndTimeAsync(int doctorId, DateOnly date, TimeOnly time);
}

