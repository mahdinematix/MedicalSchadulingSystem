using MS.Domain.Entities.Doctors;

namespace MS.Infrastructure.EFCore.Repositories;

public class DoctorRepository : GenericRepository<int , Doctor> , IDoctorRepository
{
    private readonly ApplicationContext _context;
    public DoctorRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }
}