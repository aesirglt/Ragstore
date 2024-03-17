namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Items.QueriesCommand;
using Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Items;
using static Totten.Solution.Ragstore.WebApi.Bases.BaseEndpointMethod;

public static class ItemsEndpoint
{
    const string _baseEndpoint = "Items";
    public static WebApplication ItemGetByNameEndpoint(this WebApplication app)
    {
        app.MapGet($"{_baseEndpoint}/{{name}}",
                   async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper,
                          [FromRoute] string name) =>
                   {
                       var returned = await mediator.Send(new ItemCollectionByNameQuery { Name = name });

                       return HandleQueryable<Item, ItemResumeViewModel>(returned, mapper);
                   }
        ).WithName($"Get{_baseEndpoint}/{{name}}")
        .WithTags("Items")
        .WithOpenApi();

        return app;
    }
}
