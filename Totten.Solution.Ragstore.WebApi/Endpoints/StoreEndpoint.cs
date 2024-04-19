namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;
using static Totten.Solution.Ragstore.WebApi.Bases.BaseEndpointMethod;

/*
    TODO:

    Stores:
        para buscar lojas apenas com base na hora do ultimo sniff de lojas.

 */
/// <summary>
/// Classe responsavel pelos metodos de endpoints para store
/// </summary>
public static class StoreEndpoint
{
    const string _baseEndpoint = "stores";

    /// <summary>
    /// Metodo responsavel por adicionar os endpoints de stores
    /// </summary>
    /// <param name="app">Aplicação</param>
    /// <returns>Aplicação</returns>
    public static WebApplication StoresEndpoints(this WebApplication app)
    {
        var grouped = app.StoreBatchPostEndpoint()
                         .MapGroup($"v1/{_baseEndpoint}");

        grouped
            .StoreGetEndpoint()
            .StoreGetByIdEndpoint()
            .StorePostEndpoint();

        return app;
    }
    private static RouteGroupBuilder StoreGetEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet($"", async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper)
                          => HandleQueryable<VendingStore, StoreResumeViewModel>(await mediator.Send(new StoreCollectionQuery()), mapper)
        ).WithName($"v1/Get{_baseEndpoint}")
        .WithTags("Stores")
        .WithOpenApi();

        return app;
    }

    private static RouteGroupBuilder StoreGetByIdEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet($"{{id}}", async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper,
                          [FromQuery] int id)
                          => HandleQuery<VendingStore, StoreDetailViewModel>(await mediator.Send(new StoreByIdQuery(id)), mapper)
        ).WithName($"v1/Get{_baseEndpoint}/{{id}}")
        .WithTags("Stores")
        .WithOpenApi();

        return app;
    }

    private static RouteGroupBuilder StorePostEndpoint(this RouteGroupBuilder app)
    {
        app.MapPost($"", async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper,
                          [FromBody] VendingStoreSaveCommand createDto)
                          => HandleCommand(await mediator.Send(createDto))
        ).WithName($"v1/Post{_baseEndpoint}")
        .WithTags("Stores")
        .WithOpenApi();
        //.RequireAuthorization("AgePolicy");

        return app;
    }

    private static WebApplication StoreBatchPostEndpoint(this WebApplication app)
    {
        app.MapPost($"{_baseEndpoint}-batch", async ([FromServices] IMediator mediator,
                                [FromServices] IMapper mapper,
                                [FromBody] VendingStoreSaveCommand[] createDto)
                                =>
                                {
                                    foreach (var dto in createDto)
                                    {
                                        _ = mediator.Send(createDto);
                                    }

                                    return await Task.FromResult(Results.Accepted());
                                }
        ).WithName($"v1/Post{_baseEndpoint}-batch")
        .WithTags("Stores")
        .WithOpenApi();
        //.RequireAuthorization("AgePolicy");

        return app;
    }
}
