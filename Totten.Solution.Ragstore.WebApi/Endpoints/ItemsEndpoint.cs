namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.QueriesCommand;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;
using static Totten.Solution.Ragstore.WebApi.Bases.BaseEndpointMethod;

public static class ItemsEndpoint
{
    const string _baseEndpoint = "Items";
    public static WebApplication ItemGetAllEndpoint(this WebApplication app)
    {
        app.MapGet(_baseEndpoint,
                   async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper) =>
                   {
                       var returned = await mediator.Send(new StoreCollectionQuery());

                       return HandleQueryable<Store, StoreResumeViewModel>(returned, mapper);
                   }
        ).WithName($"Get{_baseEndpoint}")
        .WithOpenApi();

        return app;
    }
}
