using Application.Repositories.ResturantApp.Base.Interface;
using Domain.Entity;
using Domain.Models.Request;

namespace Application.Repositories.ResturantApp.UserBooking.Interface;

public interface IUserBookingRepository : IAsyncRepository<UserBookings>
{
    Task<UserBookings> BookAvailableResturant(UserBookingDto userBookingDto);
}
