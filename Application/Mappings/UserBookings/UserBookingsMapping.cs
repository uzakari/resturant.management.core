using AutoMapper;
using Domain.Models.Request;

namespace Application.Mappings.UserBookings;

public class UserBookingsMapping : Profile
{
    public UserBookingsMapping()
    {
        CreateMap<UserBookingDto, Domain.Entity.UserBookings>();
    }
}
