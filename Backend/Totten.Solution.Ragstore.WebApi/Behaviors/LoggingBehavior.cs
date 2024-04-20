namespace Totten.Solution.Ragstore.WebApi.Behaviors;

using MediatR;
using Totten.Solution.Ragstore.Infra.Cross.Errors;
using Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class LoggingBehavior<TRequest, TResponse>
         : IPipelineBehavior<TRequest, Result<BusinessError, TResponse>>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, Result<BusinessError, TResponse>>> _logger;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, Result<BusinessError, TResponse>>> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<BusinessError, TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<BusinessError, TResponse>> next, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Handler {handlerName}", typeof(TRequest).Name);

        var response = await next();

        _logger.LogInformation("Request: {requestData} has response: {responseData}", request, response);

        response.IfFail(fail =>
        {
            if (fail is InternalError internalError)
            {
                _logger.LogCritical("InternalException handled");
                _logger.LogCritical("message: {messageException}, exception: {exception}", internalError.Message, internalError);
                _logger.LogCritical("InnerException: {innerException}", internalError.InnerException);
            }
            else if (fail is BusinessError baseError)
            {
                _logger.LogError("BusinessError handled");
                _logger.LogError("message: {messageError}, error: {error}", baseError.Message, baseError);
            }
            else
            {
                _logger.LogCritical("Exception handled");
                _logger.LogCritical("message: {messageException}, exception: {exception}", fail.Message, fail);
                _logger.LogCritical("InnerException: {innerException}", fail.InnerException);
            }
        });

        return response;
    }
}