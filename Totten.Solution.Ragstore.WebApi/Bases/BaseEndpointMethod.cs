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
        => result.Match(succ => Results.Accepted(), error => HandleFailure(error));
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static IResult HandleCommand<TSource>(Result<Exception, TSource> result)
        => result.Match(succ => Results.Ok(succ), error => HandleFailure(error));
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestiny"></typeparam>
    /// <param name="result"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static IResult HandleQuery<TSource, TDestiny>(Result<Exception, TSource> result, IMapper m)
        => result.Match(succ => Results.Ok(m.Map<TDestiny>(succ)), error => HandleFailure(error));
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestiny"></typeparam>
    /// <param name="result"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static IResult HandleQueryable<TSource, TDestiny>(Result<Exception, List<TSource>> result, IMapper m)
        => result.Match(succ => Results.Ok(m.ProjectTo<TDestiny>(succ.AsQueryable(), m.ConfigurationProvider)), error => HandleFailure(error));

    private static IResult HandleFailure<T>(T exception) where T : Exception
        => exception is ValidationException validationError
            ? Results.Problem(detail: JsonSerializer.Serialize(validationError.Errors), statusCode: HttpStatusCode.BadRequest.GetHashCode())
            : ErrorPayload.New<T>(exception).Apply(error => Results.Problem(detail: JsonSerializer.Serialize(error), statusCode: error.ErrorCode.GetHashCode()));
}
