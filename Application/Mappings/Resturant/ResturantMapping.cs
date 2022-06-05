using AutoMapper;
using Domain.Models.Request;

namespace Application.Mappings.Resturant;

public class ResturantMapping : Profile
{
    public ResturantMapping()
    {
        CreateMap<ResturantDto, Domain.Entity.Resturant>();

        CreateMap<Domain.Entity.Resturant, Domain.Models.Response.ResturantVM>();

        CreateMap<Domain.Entity.ResturantTable, Domain.Models.Response.ResturantTableVM>();
        CreateMap<Domain.Models.Request.ResturantTableDto, Domain.Entity.ResturantTable>();
        //CreateMap<Domain.Models.Request.ResturantTableDto, Domain.Models.Response.ResturantTableVM>();

    }
}
