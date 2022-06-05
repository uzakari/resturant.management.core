using Application.Behaviours.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI.Behaviour;

public static class BehaviourService
{
    public static IServiceCollection AddBehaviourService(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(BehaviourService).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return services;
    }
}
