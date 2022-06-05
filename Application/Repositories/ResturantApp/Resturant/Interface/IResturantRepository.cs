using Application.Repositories.ResturantApp.Base.Interface;
using Domain.Models.Request;
using Domain.Models.Response;

namespace Application.Repositories.ResturantApp.Resturant.Interface;

public interface IResturantRepository : IAsyncRepository<Domain.Entity.Resturant>
{
    Task<ResturantVM> CreateResturant(ResturantDto resturantDto);

    Task<List<Domain.Entity.Resturant>> GetAllResturantAndAvalableSeat();
}
