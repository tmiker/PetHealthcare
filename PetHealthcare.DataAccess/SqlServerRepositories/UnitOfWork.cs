using PetHealthcare.DataAccess.Data;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;

namespace PetHealthcare.DataAccess.SqlServerRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Pets = new PetRepository(_db);
            Vets = new VetRepository(_db);
            Visits = new VisitRepository(_db);
            CarouselImages = new CarouselImageRepository(_db);
            DeleteImages = new DeleteImageRepository(_db);
        }

        public IPetRepository Pets { get; private set; }
        public IVetRepository Vets { get; private set; }
        public IVisitRepository Visits { get; private set; }
        public ICarouselImageRepository CarouselImages { get; private set; }
        public IDeleteImageRepository DeleteImages { get; private set; }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
