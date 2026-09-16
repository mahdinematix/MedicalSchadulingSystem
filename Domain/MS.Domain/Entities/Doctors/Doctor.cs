using MS.Domain.Common;
using MS.Domain.Entities.Appointments;
using MS.Domain.Entities.Schedules;

namespace MS.Domain.Entities.Doctors;

public class Doctor : BaseEntity<int>
{
    public string Fullname { get; private set; }
    public string Speciality { get; private set; }
    public string MedicalSystemNumber { get; private set; }
    public List<Appointment> Appoinments{ get; private set; }
    public List<Schedule> Schedules { get; private set; }

    public Doctor(string fullname, string speciality, string medicalSystemNumber)
    {
        Fullname = fullname;
        Speciality = speciality;
        MedicalSystemNumber = medicalSystemNumber;
        Appoinments = new List<Appointment>();
        Schedules = new List<Schedule>();
    }

    public void Update(string fullname, string speciality, string medicalSystemNumber)
    {
        Fullname = fullname;
        Speciality = speciality;
        MedicalSystemNumber = medicalSystemNumber;
    }
}