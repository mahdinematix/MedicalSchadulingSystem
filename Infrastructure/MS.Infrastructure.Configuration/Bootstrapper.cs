using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MS.Application.DoctorService.CommandHandlers;
using MS.Domain.Common;
using MS.Domain.Entities.Appointments;
using MS.Domain.Entities.Doctors;
using MS.Domain.Entities.Patients;
using MS.Domain.Entities.Schedules;
using MS.Infrastructure.EFCore;
using MS.Infrastructure.EFCore.Repositories;

namespace MS.Infrastructure.Configuration;

public static class Bootstrapper
{
    public static IServiceCollection WireUp(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();

        services.AddDbContext<ApplicationContext>(x => x.UseSqlServer(connectionString));

        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CreateDoctorCommandHandler).Assembly));

        return services;
    }
}

