namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Items.Queries;
using Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Items;
using static Totten.Solution.Ragstore.WebApi.Bases.BaseEndpointMethod;

/// <summary>
/// Classe responsavel pelos endpoints de Items
/// </summary>
public static class ItemsEndpoint
{
    const string _baseEndpoint = "items";

    /// <summary>
    /// Metodo responsavel por inserir todos os endpoints de items
    /// </summary>
    /// <param name="app">Applicação para configuração dos endpoints</param>
    /// <returns>Retorna a propria aplicação</returns>
    public static WebApplication ItemsEndpoints(this WebApplication app)
    {
        app.MapGroup($"v1/{_baseEndpoint}")
           .ItemsByNameEndpoint();

        return app;
    }

    private static RouteGroupBuilder ItemsByNameEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet($"{{name}}",
                   async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper,
                          [FromRoute] string name) =>
                   {
                       var returned = await mediator.Send(new ItemCollectionByNameQuery { Name = name });

                       return HandleQueryable<Item, ItemResumeViewModel>(returned, mapper);
                   }
        ).WithName($"v1/Get{_baseEndpoint}/{{name}}")
        .WithTags("Items")
        .WithOpenApi();

        return app;
    }
}
