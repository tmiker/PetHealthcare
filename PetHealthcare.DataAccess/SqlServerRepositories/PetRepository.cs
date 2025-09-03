using PetHealthcare.DataAccess.Data;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.DataAccess.SqlServerRepositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        private readonly ApplicationDbContext _db;

        public PetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
