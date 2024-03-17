namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;
using Totten.Solution.Ragstore.Infra.Cross.CrossDTOs;
using Totten.Solution.Ragstore.WebApi.Endpoints.Dtos.Callbacks;
using static Totten.Solution.Ragstore.WebApi.Bases.BaseEndpointMethod;

/// <summary>
/// Classe responsavel pelos endpoints de Items
/// </summary>
public static class CallbacksEndpoint
{
    const string _baseEndpoint = "callbacks";

    /// <summary>
    /// Metodo responsavel por inserir todos os endpoints de items
    /// </summary>
    /// <param name="app">Applicação para configuração dos endpoints</param>
    /// <returns>Retorna a propria aplicação</returns>
    public static WebApplication CallbacksEndpoints(this WebApplication app)
    {
        app.CallbackPostEndpoint();

        return app;
    }

    private static WebApplication CallbackPostEndpoint(this WebApplication app)
    {
        app.MapPost($"{_baseEndpoint}-items", async ([FromServices] IMediator mediator,
                          [FromServices] IMapper mapper,
                          [FromBody] CallbackCreateDto createDto)
                          => HandleCommand(await mediator.Send(mapper.Map<CallbackSaveCommand>((createDto, new UserData
                          {
                              Id = $"d7aeb595-44a5-4f5d-822e-980f35ace12d",
                              Email = "aleffmds@gmail.com",
                              Cellphone = "+5584988633251",
                              Level = EUserLevel.SYSTEM
                          }))))
        ).WithName($"v1/Post{_baseEndpoint}-items")
        .WithTags("Callbacks")
        .WithOpenApi();

        return app;
    }
}
