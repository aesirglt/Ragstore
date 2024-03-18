namespace Totten.Solution.Ragstore.WebApi.Behaviors;

using FluentValidation;
using MediatR;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
/// <summary>
/// 
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<Exception, TResponse>> where TRequest : notnull
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
    public async Task<Result<Exception, TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<Exception, TResponse>> next, CancellationToken cancellationToken)
    {
        List<FluentValidation.Results.ValidationFailure> failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        return failures.Any()
               ? Result<Exception, TResponse>.Err(new ValidationException(failures))
               : await next();
    }
}
