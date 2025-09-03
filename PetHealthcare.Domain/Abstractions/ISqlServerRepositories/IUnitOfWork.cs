namespace PetHealthcare.Domain.Abstractions.ISqlServerRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPetRepository Pets { get; }
        IVetRepository Vets { get; }
        IVisitRepository Visits { get; }
        ICarouselImageRepository CarouselImages { get; }
        IDeleteImageRepository DeleteImages { get; }
        Task SaveAsync();
    }
}
