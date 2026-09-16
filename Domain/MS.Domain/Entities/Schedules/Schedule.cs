using MS.Domain.Common;
using MS.Domain.Entities.Doctors;
using MS.Domain.Exceptions;

namespace MS.Domain.Entities.Schedules;

public class Schedule : BaseEntity<int>
{
    public int DoctorId { get; private set; }
    public Doctor Doctor { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly WorkStart { get; private set; }
    public TimeOnly WorkEnd { get; private set; }

    public Schedule(int doctorId, DateOnly date, TimeOnly workStart, TimeOnly workEnd)
    {
        CheckDateAndTime(date, workStart, workEnd);

        DoctorId = doctorId;
        Date = date;
        WorkStart = workStart;
        WorkEnd = workEnd;
    }

    public void Update(int doctorId, DateOnly date, TimeOnly workStart, TimeOnly workEnd)
    {
        CheckDateAndTime(date, workStart, workEnd);

        DoctorId = doctorId;
        Date = date;
        WorkStart = workStart;
        WorkEnd = workEnd;
    }

    private static void CheckDateAndTime(DateOnly date, TimeOnly workStart, TimeOnly workEnd)
    {
        if (date < DateOnly.FromDateTime(DateTime.Today))
            throw new DateCannotInThePastException();
        if (workEnd <= workStart)
            throw new WorkEndTimeCannotLessThanWorkStartTimeException();
        if (date == DateOnly.FromDateTime(DateTime.Today) && workStart < TimeOnly.FromDateTime(DateTime.Now))
            throw new TimeCannotInThePastException();
    }
}
