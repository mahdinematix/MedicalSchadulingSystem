using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities.Appointments;
using MS.Domain.Entities.Doctors;
using MS.Domain.Entities.Patients;
using MS.Domain.Entities.Schedules;

namespace MS.Infrastructure.EFCore;

public class ApplicationContext : DbContext
{
    public DbSet<Appointment> Appointments{ get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
    {
    }
}

