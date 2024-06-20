namespace Totten.Solution.Ragstore.WebApi.Behaviors;

using FluentValidation;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
/// <summary>
/// 
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>> where TRequest : notnull
{
    private readonly IValidator<TRequest>[] _validators;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="validators"></param>
    public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        List<FluentValidation.Results.ValidationFailure> failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        return failures.Any()
               ? (InvalidObjectError)("Fail on validate request", new ValidationException(failures))
               : await next();
    }
}
