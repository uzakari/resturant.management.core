using Application.Repositories.ResturantApp.Resturant.Interface;
using Domain.Exception;
using Domain.Models.Request;
using Domain.Models.Response;
using MediatR;

namespace Application.Commands.Resturant.CreateResturant;

public record CreateResturantCommand(ResturantDto ResturantDto) : IRequest<ResturantVM>;



public class CreateResturantCommandHandler : IRequestHandler<CreateResturantCommand, ResturantVM>
{
    private readonly IResturantRepository _resturantRepository;

    public CreateResturantCommandHandler(IResturantRepository resturantRepository)
    {
        _resturantRepository = resturantRepository;
    }
    public async Task<ResturantVM> Handle(CreateResturantCommand request, CancellationToken cancellationToken)
    {
        var createResturant = await _resturantRepository.CreateResturant(request.ResturantDto);

        if (createResturant == null)
            throw new ResturantValidationException("Unable to create resturant");

        return createResturant;
    }
}