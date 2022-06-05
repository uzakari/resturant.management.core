using Application.Services.RabbitMQ.Publisher.Interface;
using MediatR;

namespace Application.Commands.UserBookings;

public record UserBookingsCommand<T>(T message) : IRequest<Unit>;


public class UserBookingsCommandHandler<TObject> : IRequestHandler<UserBookingsCommand<TObject>, Unit> where TObject : class
{
    private readonly IPublishService _publishService;

    public UserBookingsCommandHandler(IPublishService publishService)
    {
        _publishService = publishService;
    }

    public async Task<Unit> Handle(UserBookingsCommand<TObject> request, CancellationToken cancellationToken)
    {
        await _publishService.SendMessage(request.message);
        return Unit.Value;
    }
}
