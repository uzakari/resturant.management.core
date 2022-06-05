using Application.Repositories.ResturantApp.Base;
using Application.Repositories.ResturantApp.Resturant.Interface;
using Application.Repositories.ResturantApp.UserBooking.Interface;
using AutoMapper;
using Domain.DB;
using Domain.Entity;
using Domain.Exception;
using Domain.Models.Request;

namespace Application.Repositories.ResturantApp.UserBooking;

public class UserBookingRepository : BaseRepository<Domain.Entity.UserBookings>, IUserBookingRepository
{
    private readonly ResturantDBContext _resturantDBContext;
    private readonly IResturantRepository _resturantRepository;
    private readonly IMapper _mapper;

    public UserBookingRepository(ResturantDBContext resturantDBContext, IResturantRepository resturantRepository, IMapper mapper) : base(resturantDBContext)
    {
        _resturantDBContext = resturantDBContext;
        _resturantRepository = resturantRepository;
        _mapper = mapper;
    }

    public async Task<UserBookings> BookAvailableResturant(UserBookingDto userBookingDto)
    {
        var allavailableResturant = (await _resturantRepository.GetAllResturantAndAvalableSeat());

        if (allavailableResturant == null)
            throw new NotFoundException(nameof(BookAvailableResturant), nameof(UserBookingRepository));

        var resturantForUpdate = allavailableResturant.FirstOrDefault(x => x.Id == userBookingDto.ResturantID)
                                .ResturantTables.FirstOrDefault(x => x.Id == userBookingDto.ResturantTableID);
        resturantForUpdate.Available = false;

        var userBookingForCreation = _mapper.Map<Domain.Entity.UserBookings>(userBookingDto);
        
        await AddAsync(userBookingForCreation);

        return userBookingForCreation;
    }
}
