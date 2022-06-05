using Application.Repositories.ResturantApp.Base;
using Application.Repositories.ResturantApp.ResturantOwner.Interface;
using Domain.DB;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.ResturantApp.ResturantOwner;

public class ResturantOwnerRepository : BaseRepository<Domain.Entity.ResturantOwner>, IResturantOwnerRepository
{
    private readonly ResturantDBContext _resturantDBContext;

    public ResturantOwnerRepository(ResturantDBContext resturantDBContext) : base(resturantDBContext)
    {
        _resturantDBContext = resturantDBContext;
    }

    public async Task<List<Domain.Entity.ResturantOwner>> GetAllResturantOwnerAsync()
    {
        var resturantOwners = await _resturantDBContext.ResturantOwner.Include(x => x.Resturants).ThenInclude(x => x.ResturantTables).ToListAsync();
        return resturantOwners;
    }

    public async Task<Domain.Entity.ResturantOwner> GetResturantOwner(string email)
    {
        return await _resturantDBContext.ResturantOwner.FirstOrDefaultAsync(x => x.Email == email);
    }
}
