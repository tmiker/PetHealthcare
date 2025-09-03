using PetHealthcare.DataAccess.Data;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.DataAccess.SqlServerRepositories
{
    public class CarouselImageRepository : Repository<CarouselImage>, ICarouselImageRepository
    {
        private readonly ApplicationDbContext _db;

        public CarouselImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
