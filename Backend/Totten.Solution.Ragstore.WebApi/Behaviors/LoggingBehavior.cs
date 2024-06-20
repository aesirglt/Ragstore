namespace Totten.Solution.Ragstore.WebApi.Behaviors;

using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class LoggingBehavior<TRequest, TResponse>
         : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, Result<TResponse>>> _logger;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, Result<TResponse>>> logger)
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
    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Handler {handlerName}", typeof(TRequest).Name);

        var response = await next();

        _logger.LogInformation("Request: {requestData} has response: {responseData}", request, response);

        response.Else(fail =>
        {
            if (fail is UnhandledError internalError)
            {
                _logger.LogCritical("InternalException handled");
                _logger.LogCritical("message: {messageException}, exception: {exception}", internalError.Message, internalError.Exception);
                _logger.LogCritical("InnerException: {innerException}", internalError.Exception?.InnerException);
            }
            else if (fail is BaseError baseError)
            {
                _logger.LogError("BusinessError handled");
                _logger.LogError("message: {messageError}, error: {error}", baseError.Message, baseError.Exception);
            }
        });

        return response;
    }
}