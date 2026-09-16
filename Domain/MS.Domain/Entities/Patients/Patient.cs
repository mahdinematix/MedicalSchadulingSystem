using MS.Domain.Common;
using MS.Domain.Entities.Appointments;

namespace MS.Domain.Entities.Patients;

public class Patient : BaseEntity<int>
{
    public string Fullname { get; private set; }
    public string Mobile { get; private set; }
    public List<Appointment> Appoinments { get; set; }

    public Patient(string fullname, string mobile)
    {
        Fullname = fullname;
        Mobile = mobile;
        Appoinments = new List<Appointment>();
    }

    public void Update(string fullname, string mobile)
    {
        Fullname = fullname;
        Mobile = mobile;
    }
}

