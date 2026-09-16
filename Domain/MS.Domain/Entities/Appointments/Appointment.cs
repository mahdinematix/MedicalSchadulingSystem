using MS.Domain.Common;
using MS.Domain.Entities.Doctors;
using MS.Domain.Entities.Patients;
using MS.Domain.Exceptions;

namespace MS.Domain.Entities.Appointments;

public class Appointment : BaseEntity<int>
{
    public int PatientId { get;  set; }
    public Patient Patient { get;  set; }
    public int DoctorId { get;  set; }
    public Doctor Doctor { get;  set; }
    public DateOnly Date { get;  set; }
    public TimeOnly Time { get;  set; }
    public AppointmentStatus Status { get;  set; }

    public Appointment(int patientId, int doctorId, DateOnly date, TimeOnly time)
    {
        CheckDateAndTime(date, time);

        PatientId = patientId;
        DoctorId = doctorId;
        Date = date;
        Time = time;
        Status = AppointmentStatus.Reserved;
    }

    public void ReSchedule(DateOnly date, TimeOnly time)
    {
        CheckDateAndTime(date, time);


        Date = date;
        Time = time;
        Status = AppointmentStatus.Reserved;
    }

    public void Cancel()
    {
        Status = AppointmentStatus.Cancelled;
    }

     static void CheckDateAndTime(DateOnly date, TimeOnly time)
    {
        if (date < DateOnly.FromDateTime(DateTime.Today))
            throw new DateCannotInThePastException();

        if (date == DateOnly.FromDateTime(DateTime.Today) && time < TimeOnly.FromDateTime(DateTime.Now))
            throw new TimeCannotInThePastException();
    }
}

