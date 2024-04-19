namespace Totten.Solution.Ragstore.WebApi.Bases;

using AutoMapper;
using FluentValidation;
using System.Net;
using System.Text.Json;
using Totten.Solution.Ragstore.Infra.Cross.Errors;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

/// <summary>
/// 
/// </summary>
public static class BaseEndpointMethod
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static IResult HandleAccepted<TSource>(Result<Exception, TSource> result)
        => result.Match(succ => Results.Accepted(), HandleFailure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static IResult HandleCommand<TSource>(Result<Exception, TSource> result)
        => result.Match(succ => Results.Ok(succ), HandleFailure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestiny"></typeparam>
    /// <param name="result"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static IResult HandleQuery<TSource, TDestiny>(Result<Exception, TSource> result, IMapper m)
        => result.Match(succ => Results.Ok(m.Map<TDestiny>(succ)), HandleFailure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestiny"></typeparam>
    /// <param name="result"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static IResult HandleQueryable<TSource, TDestiny>(Result<Exception, IQueryable<TSource>> result, IMapper m)
        => result.Match(succ => Results.Ok(m.ProjectTo<TDestiny>(succ, m.ConfigurationProvider)), HandleFailure);

    private static IResult HandleFailure(Exception exception)
        => exception is ValidationException validationError
            ? Results.Problem(title: "ValidationError",
                              detail: JsonSerializer.Serialize(validationError.Errors),
                              extensions: new Dictionary<string, object?>
                              {
                                  { "TraceId", $"{Guid.NewGuid()}" },
                                  { "TraceI1d", $"{Guid.NewGuid()}" },
                                  { "TraceId2", $"{Guid.NewGuid()}" }
                              }, statusCode: HttpStatusCode.BadRequest.GetHashCode())
            : ErrorPayload.New(exception)
                          .Apply(error => Results.Problem(title: $"{exception.GetType().Name}",
                                                          detail: error.ErrorMessage,
                                                          extensions: new Dictionary<string, object?>
                                                          {
                                                              { "TraceId", $"{Guid.NewGuid()}" },
                                                              { "TraceI1d", $"{Guid.NewGuid()}" },
                                                              { "TraceId2", $"{Guid.NewGuid()}" }
                                                          }, statusCode: error.ErrorCode.GetHashCode()));
}
