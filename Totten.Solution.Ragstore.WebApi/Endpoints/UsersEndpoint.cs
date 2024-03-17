namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Features.Items.Queries;
using static Totten.Solution.Ragstore.WebApi.Bases.BaseEndpointMethod;

/// <summary>
/// Classe responsavel pelos endpoints de Items
/// </summary>
public static class UsersEndpoint
{
    const string _baseEndpoint = "users";

    /// <summary>
    /// Metodo responsavel por inserir todos os endpoints de items
    /// </summary>
    /// <param name="app">Applicação para configuração dos endpoints</param>
    /// <returns>Retorna a propria aplicação</returns>
    public static WebApplication UsersEndpoints(this WebApplication app)
    {
        app.UsersGetEndpoint();

        return app;
    }
    private static WebApplication UsersGetEndpoint(this WebApplication app)
    {
        app.MapGet($"{_baseEndpoint}",
                    async ([FromServices] IMediator mediator)
                          => HandleCommand(await mediator.Send(new CallbackCollectionByUserIdQuery { UserId = "d7aeb595-44a5-4f5d-822e-980f35ace12d" }))
        ).WithName($"v1/Get{_baseEndpoint}")
        .WithTags("Users")
        .WithOpenApi();

        return app;
    }
}
