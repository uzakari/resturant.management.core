using Application.Repositories.ResturantApp.Resturant.Interface;
using AutoMapper;
using Domain.Exception;
using Domain.Models.Response;
using MediatR;

namespace Application.Queries.Resturant.GetAllResturantWithAvailableTable;

public record GetAllResturantWithAvailableTableQuery() : IRequest<List<ResturantVM>>;



public class GetAllResturantWithAvailableQueryHandler : IRequestHandler<GetAllResturantWithAvailableTableQuery, List<ResturantVM>>
{
    private readonly IResturantRepository _resturantRepository;
    private readonly IMapper _mapper;

    public GetAllResturantWithAvailableQueryHandler(IResturantRepository resturantRepository, IMapper mapper)
    {
        _resturantRepository = resturantRepository;
        _mapper = mapper;
    }
    public async Task<List<ResturantVM>> Handle(GetAllResturantWithAvailableTableQuery request, CancellationToken cancellationToken)
    {
        var toReturn = await _resturantRepository.GetAllResturantAndAvalableSeat();

        if (toReturn == null)
            throw new NotFoundException(nameof(GetAllResturantWithAvailableQueryHandler), nameof(GetAllResturantWithAvailableTableQuery));
        return _mapper.Map<List<ResturantVM>>(toReturn);
    }
}
