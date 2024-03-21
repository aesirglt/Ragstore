namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.HandlersCommand;
using Totten.Solution.Ragstore.ApplicationService.Features.Stores.QueriesCommand;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.WebApi.Endpoints.Dtos.Stores;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Stores;
using static Totten.Solution.Ragstore.WebApi.Bases.BaseEndpointMethod;

/*
    TODO:

    Stores:
        Para evitar lojas duplicadas na consulta adicionar um filtro no where
        para buscar lojas apenas com base na hora do ultimo sniff de lojas.

    Items:
        Na collection de items um endpoint vai acessar com base no ultimo sniff para verificar se esta sendo vendido atualmente.
        Outro endpoint vai acessar sem filtro para acessar como historico de preço do item.
 */
/// <summary>
/// Classe responsavel pelos metodos de endpoints para store
/// </summary>
public static class StoresEndpoint
{
    const string _baseEndpoint = "stores";

    /// <summary>
    /// Metodo responsavel por adicionar os endpoints de stores
    /// </summary>
    /// <param name="app">Aplicação</param>
    /// <returns>Aplicação</returns>
    public static WebApplication StoresEndpoints(this WebApplication app)
    {
        app.StorePostBatchEndpoint();

        var grouped = app.MapGroup($"v1/{_baseEndpoint}");

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
                          => HandleQueryable<VendingStore, StoreResumeViewModel>(await mediator.Send(new StoreCollectionQuery()),mapper)
        ).WithName($"v1/Get{_baseEndpoint}")
        .WithTags("Stores")
        .WithOpenApi();

        return app;
    }

    private static RouteGroupBuilder StoreGetByIdEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet($"{{id}}", async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper,
                          [FromQuery] Guid id)
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
                          [FromBody] StoreCreateDto createDto)
                          => HandleCommand(await mediator.Send(mapper.Map<StoreSaveCommand>(createDto)))
        ).WithName($"v1/Post{_baseEndpoint}")
        .WithTags("Stores")
        .WithOpenApi()
        .RequireAuthorization("AgePolicy");

        return app;
    }

    private static WebApplication StorePostBatchEndpoint(this WebApplication app)
    {
        app.MapPost($"v1/{_baseEndpoint}-batch",
                   async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper,
                          [FromBody] List<StoreCreateDto> createDto) =>
                   {
                       var batchCommand = new StoreSaveBatchCommand(mapper.ProjectTo<StoreSaveCommand>(createDto.AsQueryable()));
                       return HandleCommand(await mediator.Send(batchCommand));
                   }
        ).WithName($"v1/Post{_baseEndpoint}-batch")
        .WithTags("Stores")
        .WithOpenApi()
        .RequireAuthorization();

        return app;
    }

}
