namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.HandlersCommand;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.QueriesCommand;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.WebApi.Endpoints.Dtos.Stores;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;
using static Totten.Solution.Ragstore.WebApi.Bases.BaseEndpointMethod;

public static class StoresEndpoint
{
    const string _baseEndpoint = "Stories";
    public static WebApplication StoreGetEndpoint(this WebApplication app)
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

    public static WebApplication StoreGetByIdEndpoint(this WebApplication app)
    {
        app.MapGet($"{_baseEndpoint}/{{id}}",
                   async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper,
                          [FromQuery] string id) =>
                   {
                       var returned = await mediator.Send(new StoreByIdQuery { Id = id });

                       return HandleQuery<Store, StoreDetailViewModel>(returned, mapper);
                   }
        ).WithName($"Get{_baseEndpoint}/{{id}}")
        .WithOpenApi();

        return app;
    }

    public static WebApplication StorePostEndpoint(this WebApplication app)
    {
        app.MapPost($"{_baseEndpoint}",
                   async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper,
                          [FromBody] StoreCreateDto createDto) =>
                   {
                       return HandleCommand(await mediator.Send(mapper.Map<StoreSaveCommand>(createDto)));
                   }
        ).WithName($"Post{_baseEndpoint}")
        .WithOpenApi();

        return app;
    }

}
