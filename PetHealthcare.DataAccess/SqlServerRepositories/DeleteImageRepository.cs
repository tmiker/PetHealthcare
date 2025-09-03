using PetHealthcare.DataAccess.Data;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.DataAccess.SqlServerRepositories
{
    public class DeleteImageRepository : Repository<DeleteImage>, IDeleteImageRepository
    {
        private readonly ApplicationDbContext _db;

        public DeleteImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
