namespace Totten.Solution.Ragstore.WebApi.Bases;

using AutoMapper;
using FluentValidation;
using System.Net;
using System.Text.Json;
using Totten.Solution.Ragstore.Infra.Cross.Errors;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public static class BaseEndpointMethod
{
    public static IResult HandleAccepted<TSource>(Result<Exception, TSource> result)
        => result.Match(succ => Results.Accepted(), error => HandleFailure(error));
    public static IResult HandleCommand<TSource>(Result<Exception, TSource> result)
        => result.Match(succ => Results.Ok(succ), error => HandleFailure(error));

    public static IResult HandleQuery<TSource, TDestiny>(Result<Exception, TSource> result, IMapper m)
        => result.Match(succ => Results.Ok(m.Map<TDestiny>(succ)), error => HandleFailure(error));
    public static IResult HandleQueryable<TSource, TDestiny>(Result<Exception, List<TSource>> result, IMapper m)
        => result.Match(succ => Results.Ok(m.ProjectTo<TDestiny>(succ.AsQueryable(), m.ConfigurationProvider)), error => HandleFailure(error));

    private static IResult HandleFailure<T>(T exception) where T : Exception
        => exception is ValidationException validationError
            ? Results.Problem(detail: JsonSerializer.Serialize(validationError.Errors), statusCode: HttpStatusCode.BadRequest.GetHashCode())
            : ErrorPayload.New(exception).Apply(error => Results.Problem(detail: JsonSerializer.Serialize(error), statusCode: error.ErrorCode.GetHashCode()));
}
