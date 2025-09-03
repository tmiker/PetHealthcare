using PetHealthcare.DataAccess.Data;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.DataAccess.SqlServerRepositories
{
    public class VetRepository : Repository<Vet>, IVetRepository
    {
        private readonly ApplicationDbContext _db;

        public VetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
