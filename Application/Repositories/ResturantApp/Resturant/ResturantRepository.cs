using Application.Repositories.ResturantApp.Base;
using Application.Repositories.ResturantApp.Resturant.Interface;
using AutoMapper;
using Domain.Constants;
using Domain.DB;
using Domain.Exception;
using Domain.Models.Request;
using Domain.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.ResturantApp.Resturant;

public class ResturantRepository : BaseRepository<Domain.Entity.Resturant>, IResturantRepository
{
    private readonly ResturantDBContext _resturantDBContext;
    private readonly IMapper _mapper;

    public ResturantRepository(ResturantDBContext resturantDBContext, IMapper mapper) : base(resturantDBContext)
    {
        _resturantDBContext = resturantDBContext;
        _mapper = mapper;
    }

    public async Task<ResturantVM> CreateResturant(ResturantDto resturantDto)
    {
        var resturantOwner = await _resturantDBContext.ResturantOwner.FirstOrDefaultAsync(x => x.Email == resturantDto.ResturantOwnerEmail);
       
        if (resturantOwner is null)
            throw new NotFoundException(nameof(CreateResturant) + "not owner" , nameof(ResturantRepository));

        var resturantForCreation = _mapper.Map<Domain.Entity.Resturant>(resturantDto);
        resturantForCreation.Owner = resturantOwner;

        var resturantToReturn = await _resturantDBContext.Resturants.AddAsync(resturantForCreation);

        await _resturantDBContext.SaveChangesAsync();

        return _mapper.Map<ResturantVM>(resturantToReturn.Entity);
      }

    public async Task<List<Domain.Entity.Resturant>> GetAllResturantAndAvalableSeat()
    {
        var resturantToReturn = await _resturantDBContext.Resturants
                                        .Include(x => x.ResturantTables.Where(x => x.Available == ResturantConstants.Available))
                                        .Include(x => x.Owner).ToListAsync();

        return resturantToReturn; 
    }
}
