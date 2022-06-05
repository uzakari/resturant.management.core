using Application.Repositories.ResturantApp.Base.Interface;

namespace Application.Repositories.ResturantApp.ResturantOwner.Interface;

public interface IResturantOwnerRepository : IAsyncRepository<Domain.Entity.ResturantOwner>
{
    Task<List<Domain.Entity.ResturantOwner>> GetAllResturantOwnerAsync();

    Task<Domain.Entity.ResturantOwner> GetResturantOwner(string email);
}
