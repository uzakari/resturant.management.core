using AutoMapper;
using Domain.Extension;
using Domain.Models.Request;
using Domain.Models.Response;

namespace Application.Mappings.ResturantOwner;

public class ResturantOwnerMapping : Profile
{
    public ResturantOwnerMapping()
    {
        CreateMap<ResturantOwnerDto, Domain.Entity.ResturantOwner>()
            .ForMember(o => o.PasswordSalt, src => src.MapFrom(f => f.Password.HashedPassword()))
            .ForMember(o => o.Password, src => src.MapFrom(f => f.Password.HashedPassword()));

        CreateMap<Domain.Entity.ResturantOwner, ResturantOwnerVM>()
            .ForMember(o => o.FullName, src => src.MapFrom(f => $"{f.FirstName} {f.LastName}"));

        CreateMap<Domain.Entity.ResturantOwner, ResturantOwnerWithResturantVM>()
            .ForMember(o => o.FullName, src => src.MapFrom(f => $"{f.FirstName} {f.LastName}"));
    }
}
