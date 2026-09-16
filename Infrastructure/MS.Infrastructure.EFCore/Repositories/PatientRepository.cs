using MS.Domain.Entities.Patients;

namespace MS.Infrastructure.EFCore.Repositories;

public class PatientRepository : GenericRepository<int, Patient>, IPatientRepository
{
    private readonly ApplicationContext _context;
    public PatientRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }
}