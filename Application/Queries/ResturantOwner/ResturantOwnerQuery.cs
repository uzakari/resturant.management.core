using Application.Repositories.ResturantApp.ResturantOwner.Interface;
using AutoMapper;
using Domain.Models.Response;
using MediatR;

namespace Application.Queries.ResturantOwner;

public record ResturantOwnerQuery() : IRequest<List<ResturantOwnerWithResturantVM>>;


public class ResturantOwnerQueryHandler : IRequestHandler<ResturantOwnerQuery, List<ResturantOwnerWithResturantVM>>
{
    private readonly IResturantOwnerRepository _resturantOwnerRepository;
    private readonly IMapper _mapper;

    public ResturantOwnerQueryHandler(IResturantOwnerRepository resturantOwnerRepository, IMapper mapper)
    {
        _resturantOwnerRepository = resturantOwnerRepository;
        _mapper = mapper;
    }
    public async Task<List<ResturantOwnerWithResturantVM>> Handle(ResturantOwnerQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<ResturantOwnerWithResturantVM>>(await _resturantOwnerRepository.GetAllResturantOwnerAsync());
    }
}

