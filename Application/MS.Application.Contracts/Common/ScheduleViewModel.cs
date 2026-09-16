namespace MS.Application.Contracts.Common
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly WorkStart { get; set; }
        public TimeOnly WorkEnd { get; set; }
    }
}
