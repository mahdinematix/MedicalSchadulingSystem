namespace MS.Application.Contracts.Common;

public class AppointmentViewModel
{
    public int PatientId { get; set; }
    public string PatientName { get; set; }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string Status { get; set; }
}

