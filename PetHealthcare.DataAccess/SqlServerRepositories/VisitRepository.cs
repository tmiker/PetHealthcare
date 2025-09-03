using PetHealthcare.DataAccess.Data;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.DataAccess.SqlServerRepositories
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        private readonly ApplicationDbContext _db;

        public VisitRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
