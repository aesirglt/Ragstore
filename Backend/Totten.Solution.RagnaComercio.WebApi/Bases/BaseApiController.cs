namespace Totten.Solution.RagnaComercio.WebApi.Bases;

using Autofac;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using FunctionalConcepts;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Options;
using FunctionalConcepts.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Totten.Solution.RagnaComercio.ApplicationService.Notifications.ODataFilters;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;
using Totten.Solution.RagnaComercio.Infra.Cross.Errors;
using Totten.Solution.RagnaComercio.WebApi.Dtos;
using Totten.Solution.RagnaComercio.WebApi.Filters;
using Totten.Solution.RagnaComercio.WebApi.Modules;

/// <summary>
/// 
/// </summary>
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    protected ILifetimeScope _currentGlobalScoped;
    /// <summary>
    /// 
    /// </summary>
    protected IMapper _mapper;
    /// <summary>
    /// 
    /// </summary>
    protected IMediator _mediator;
    /// <summary>
    /// 
    /// </summary>
    protected IServerRepository _serverRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetimeScope"></param>
    public BaseApiController(ILifetimeScope lifetimeScope)
    {
        _currentGlobalScoped = lifetimeScope;
        _serverRepository = _currentGlobalScoped.Resolve<IServerRepository>();
        _mapper = _currentGlobalScoped.Resolve<IMapper>();
        _mediator = _currentGlobalScoped.Resolve<IMediator>();
    }

    /// <summary>
    /// 
    /// </summary>
    public Guid UserId => Guid.TryParse(User.Claims.FirstOrDefault(c => c?.Type is "sub" or "id")?.Value, out var result) ? result : Guid.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string UserEmail => User.Claims.FirstOrDefault(c => c?.Type is ClaimTypes.Email)?.Value ?? string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string UserNormalizedEmail => User.Claims.FirstOrDefault(c => c?.Type is "NormalizedEmail")?.Value ?? string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <returns></returns>
    private ILifetimeScope CreateChildScope(string server)
        => _currentGlobalScoped.BeginLifetimeScope(builder =>
        {
            builder.RegisterModule(new TenantModule
            {
                Server = server,
            });
        });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="serverName"></param>
    /// <returns></returns>
    protected async Task<IActionResult> HandleEvent(
        Func<ILifetimeScope, INotification> notification,
        string serverName)
    {
        return await _serverRepository
            .GetByName(serverName)
            .MatchAsync(async succ =>
            {
                try
                {
                    var scope = CreateChildScope(serverName);
                    var mediator = scope.Resolve<IMediator>();
                    await mediator.Publish(notification(scope));

                    return Accepted();
                }
                catch (Exception ex)
                {
                    UnhandledError err = (ex.Message, ex);
                    return HandleFailure(err);
                }
            }, () => HandleFailure(ServerNotFound()));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    protected async Task<IActionResult> HandleEvent(INotification notification)
    {
        try
        {
            await _mediator.Publish(notification);

            return Accepted();
        }
        catch (Exception ex)
        {
            UnhandledError err = (ex.Message, ex);
            return HandleFailure(err);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cmds"></param>
    /// <param name="serverName"></param>
    /// <returns></returns>
    protected async Task<IActionResult> HandleAccepted(
        string serverName,
        params IRequest<Result<Success>>[] cmds)
        => await _serverRepository
            .GetByName(serverName)
            .MatchAsync(async succ =>
            {
                var scope = CreateChildScope(serverName);
                var mediator = scope.Resolve<IMediator>();
                try
                {
                    foreach (var cmd in cmds)
                    {
                        _ = mediator.Send(cmd);
                    }

                    return await Task.FromResult(Accepted());
                }
                catch (Exception ex)
                {
                    UnhandledError err = (ex.Message, ex);
                    return await Task.FromResult(HandleFailure(err));
                }
            }, () => HandleFailure(ServerNotFound()));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="cmd"></param>
    /// <returns></returns>
    protected async Task<IActionResult> HandleCommand(Guid serverId, IRequest<Result<Success>> cmd)
    {
        return await ( await _serverRepository
            .GetById(serverId) )
            .MatchAsync(async server =>
            {
                var scope = CreateChildScope(server.Name);
                var mediator = scope.Resolve<IMediator>();
                var result = await mediator.Send(cmd);

                return result.Match(succ => Ok(succ), HandleFailure)!;
            }, () => HandleFailure(ServerNotFound()));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>

    protected async Task<IActionResult> HandleCommand(IRequest<Result<Success>> cmd)
    {
        var result = await _mediator.Send(cmd);
        return result.Match(succ => Ok(succ), HandleFailure)!;
    }
    private async Task<IActionResult> HandleQueryInternal<TSource, TDestiny>(
    Func<Task<Option<Server>>>? getServerFunc, IRequest<Result<TSource>> query, bool useMapper)
    {
        if (getServerFunc != null)
        {
            var serverOption = await getServerFunc();
            return await serverOption.MatchAsync(async server =>
            {
                var scope = CreateChildScope(server.Name);
                var result = await scope.Resolve<IMediator>().Send(query);
                return result.Match(
                    succ => useMapper
                        ? Ok(scope.Resolve<IMapper>().Map<TDestiny>(succ))
                        : Ok((object?)succ),
                    HandleFailure
                )!;
            }, () => HandleFailure(ServerNotFound()));
        }
        var result = await _currentGlobalScoped.Resolve<IMediator>().Send(query);
        return result.Match(
            succ => useMapper
                ? Ok(_currentGlobalScoped.Resolve<IMapper>().Map<TDestiny>(succ))
                : Ok((object?)succ),
            HandleFailure
        )!;
    }
    protected Task<IActionResult> HandleQuery<TSource, TDestiny>(IRequest<Result<TSource>> query)
        => HandleQueryInternal<TSource, TDestiny>(null, query, useMapper: true);

    protected Task<IActionResult> HandleQuery<TSource>(
        IRequest<Result<TSource>> query, Guid serverId)
        => HandleQueryInternal<TSource, TSource>(() => _serverRepository.GetById(serverId), query, useMapper: false);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="query"></param>
    /// <param name="serverName"></param>
    /// <returns></returns>
    protected async Task<IActionResult> HandleQuery<TSource>(
        IRequest<Result<TSource>> query,
        string serverName)
    {
        return await _serverRepository
            .GetByName(serverName)
            .MatchAsync(async succ =>
            {
                var scope = CreateChildScope(serverName);
                var mediator = scope.Resolve<IMediator>();
                var result = await mediator.Send(query);

                return result.Match(succ => Ok(succ), HandleFailure)!;
            }, () => HandleFailure(ServerNotFound()));
    }

    private async Task<IActionResult> HandleQueryable<TSource, TDestiny>(
    Func<Task<Option<Server>>>? getServerFunc, IRequest<Result<IQueryable<TSource>>> query,
    ODataQueryOptions<TDestiny> queryOptions)
    {
        if (getServerFunc != null)
        {
            var serverOption = await getServerFunc();
            return await serverOption.MatchAsync(async server =>
            {
                var scope = CreateChildScope(server.Name);
                var result = await scope.Resolve<IMediator>().Send(query);
                return result.Match(
                    succ => Ok(HandlePageAsync(succ, queryOptions, scope.Resolve<IMapper>())),
                    HandleFailure
                )!;
            },
            () => HandleFailure(ServerNotFound()));
        }

        var result = await _currentGlobalScoped.Resolve<IMediator>().Send(query);
        return result.Match(
            succ => Ok(HandlePageAsync(succ, queryOptions, _currentGlobalScoped.Resolve<IMapper>())),
            HandleFailure
        )!;
    }

    protected Task<IActionResult> HandleQueryable<TSource, TDestiny>(
    IRequest<Result<IQueryable<TSource>>> query, ODataQueryOptions<TDestiny> queryOptions, Guid serverId)
        => HandleQueryable(() => _serverRepository.GetById(serverId), query, queryOptions);

    protected Task<IActionResult> HandleQueryable<TSource, TDestiny>(
        IRequest<Result<IQueryable<TSource>>> query, ODataQueryOptions<TDestiny> queryOptions)
        => HandleQueryable(null, query, queryOptions);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TView"></typeparam>
    /// <param name="query"></param>
    /// <param name="mapper"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    private PaginationDto<TView> HandlePageAsync<TDomain, TView>(
    IQueryable<TDomain> query,
    ODataQueryOptions<TView> queryOptions,
    IMapper mapper)
    {
        IQueryable<TView> projectedQuery =
            typeof(TDomain) != typeof(TView)
            ? query.ProjectTo<TView>(mapper.ConfigurationProvider)
            : (IQueryable<TView>)query;

        var filteredQuery = projectedQuery;

        var odataSettings = new ODataQuerySettings
        {
            HandleNullPropagation = HandleNullPropagationOption.False,
        };

        if (queryOptions.Filter != null)
        {
            filteredQuery = (IQueryable<TView>)queryOptions.Filter.ApplyTo(filteredQuery, odataSettings);

            var match = Regex.Match(HttpContext.Request.QueryString.Value ?? "", @"(?:\?|&)storeType=([^&]+)");
            if (match.Success)
            {
                foreach (var id in ODataHelper.ExtractItemIdsFromFilter(queryOptions.Filter.RawValue))
                {
                    _mediator.Publish(new ODataFilterNotification
                    {
                        Type = match.Groups[1].Value,
                        ItemId = int.Parse(id),
                    }, CancellationToken.None);
                }
            }
        }

        if (queryOptions.OrderBy != null)
            filteredQuery = queryOptions.OrderBy.ApplyTo(filteredQuery, odataSettings);

        var queryResults = queryOptions.ApplyTo(projectedQuery);

        return new PaginationDto<TView>
        {
            Data = [.. queryResults.Provider.CreateQuery<TView>(queryResults.Expression)],
            TotalCount = filteredQuery.Count()
        };
    }

    private IActionResult HandleFailure(BaseError error)
        => error.Exception is ValidationException validationError
            ? Problem(title: "ValidationError",
                      detail: JsonConvert.SerializeObject(validationError.Errors),
                      statusCode: HttpStatusCode.BadRequest.GetHashCode())
            : MakePayload(error);
    private IActionResult MakePayload(BaseError error)
    {
        var payload = ErrorPayload.New(error.Exception, error.Message, error.Code);

        return Problem(title: $"{error.Exception?.GetType().Name}",
                       detail: payload.ErrorMessage,
                       statusCode: error.Code.GetHashCode());
    }
    private NotFoundError ServerNotFound() => "Server not found";
}
