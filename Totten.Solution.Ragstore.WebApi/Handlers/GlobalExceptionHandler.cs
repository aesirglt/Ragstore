namespace Totten.Solution.Ragstore.WebApi.Endpoints;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await Results.Problem(title: "Internal server error",
            statusCode: StatusCodes.Status500InternalServerError,
            extensions: new Dictionary<string, object?>
            {
                { "", "" }
            })
        .ExecuteAsync(httpContext);

        return true;
    }
}