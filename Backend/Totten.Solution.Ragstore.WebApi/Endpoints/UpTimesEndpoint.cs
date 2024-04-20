namespace Totten.Solution.Ragstore.WebApi.Endpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Agents;
using Unit = Infra.Cross.Functionals.Unit;

/// <summary>
/// Classe responsavel pelos metodos de endpoints para store
/// </summary>
public static class UpdateTimesEndpoint
{
    const string _baseEndpoint = "uptimes";

    /// <summary>
    /// Metodo responsavel por adicionar os endpoints de stores
    /// </summary>
    /// <param name="app">Aplicação</param>
    /// <returns>Aplicação</returns>
    public static WebApplication UpdateTimesEndpoints(this WebApplication app)
    {
        app.MapPost($"v1/{_baseEndpoint}/{{server}}",
                    async ([FromServices] IMediator mediator,
                           [FromServices] IMapper mapper,
                           [FromRoute] string server,
                           [FromBody] Unit emptyBody)
                    =>
                    {
                        await mediator.Publish(new UpdateTimeNotification
                        {
                            Server = server,
                            UpdatedAt = DateTime.Now
                        });

                        return Results.Accepted();
                    }
            ).WithName($"v1/Post{_baseEndpoint}")
            .WithTags("UpTimes")
            .WithOpenApi();

        return app;
    }
}
