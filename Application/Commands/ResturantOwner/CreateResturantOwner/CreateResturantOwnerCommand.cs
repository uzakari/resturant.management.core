using Application.Repositories.ResturantApp.Base.Interface;
using Application.Repositories.ResturantApp.ResturantOwner.Interface;
using AutoMapper;
using Domain.Exception;
using Domain.Models.Request;
using Domain.Models.Response;
using MediatR;

namespace Application.Commands.ResturantOwner.CreateResturantOwner;

public record CreateResturantOwnerCommand(ResturantOwnerDto ResturantOwnerDto) : IRequest<ResturantOwnerVM>;



public class CreateResturantOwnerCommandHandler : IRequestHandler<CreateResturantOwnerCommand, ResturantOwnerVM>
{
    private readonly IAsyncRepository<Domain.Entity.ResturantOwner> _asyncRepository;
    private readonly IMapper _mapper;
    private readonly IResturantOwnerRepository _resturantOwnerRepository;

    public CreateResturantOwnerCommandHandler(IAsyncRepository<Domain.Entity.ResturantOwner> asyncRepository, IMapper mapper, IResturantOwnerRepository resturantOwnerRepository)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
        _resturantOwnerRepository = resturantOwnerRepository;
    }
    public async Task<ResturantOwnerVM> Handle(CreateResturantOwnerCommand request, CancellationToken cancellationToken)
    {
        var checkIfAlreadyRegister = await _resturantOwnerRepository.GetResturantOwner(request.ResturantOwnerDto.Email);

        if (checkIfAlreadyRegister is not null)
            throw new ResturantValidationException("Account Already Register");

        var resturantOwnerForCreation = _mapper.Map<Domain.Entity.ResturantOwner>(request.ResturantOwnerDto);

        return _mapper.Map<ResturantOwnerVM>(await _asyncRepository.AddAsync(resturantOwnerForCreation));

    }
}
