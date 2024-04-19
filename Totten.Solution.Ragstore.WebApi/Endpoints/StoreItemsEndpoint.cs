namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;
using static Totten.Solution.Ragstore.WebApi.Bases.BaseEndpointMethod;

/// <summary>
/// Classe responsavel pelos metodos de endpoints para store
/// </summary>
public static class StoreItemsEndpoint
{
    const string _baseEndpoint = "stores-items";

    /// <summary>
    /// Metodo responsavel por adicionar os endpoints de stores
    /// </summary>
    /// <param name="app">Aplicação</param>
    /// <returns>Aplicação</returns>
    public static WebApplication StoresItemsEndpoints(this WebApplication app)
    {
        app.MapGet($"v1/{_baseEndpoint}",
                    async ([FromServices] IMediator mediator,
                           [FromServices] IMapper mapper,
                           [FromQuery] string itemName)
                    => HandleQueryable<StoreItemResponseModel, StoreItemResponseModel>(await mediator.Send(new StoreItemsCollectionQuery { ItemName = itemName }), mapper)
            ).WithName($"v1/Get{_baseEndpoint}")
            .WithTags("Stores")
            .WithOpenApi();

        return app;
    }
}
