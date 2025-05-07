namespace Totten.Solution.RagnaComercio.WebApi.Controllers;

using FunctionalConcepts.Results;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Users.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Users.Queries;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

/// <summary>
/// 
/// </summary>
/// <returns></returns>
[Route("auth")]
public class AuthController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("google")]
    public IActionResult LoginWithGoogle([FromQuery] string redirect = "/")
    {
        var redirectUrl = Url.Action(nameof(ResponseLogin), new { redirect });
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        }, "Google");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("discord")]
    public IActionResult LoginWithDiscord()
    {
        var redirectUrl = Url.Action(nameof(ResponseLogin));
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        }, "Discord");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("response")]
    public async Task<IActionResult> ResponseLogin([FromQuery] string redirect = "/")
    {
        var allowedOrigin = "http://localhost:3000";
        var authenticateResult = await HttpContext.AuthenticateAsync();

        if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            return BadRequest("Erro ao autenticar com o Google.");

        var user = authenticateResult.Principal;
        var email = user.FindFirst(ClaimTypes.Email)!.Value;
        var normalizedEmail = user.FindFirst(ClaimTypes.Email)!.Value.ToUpper();

        var userResult = await _mediator.Send(new UserByEmailQuery
        {
            NormalizedEmail = normalizedEmail
        }, CancellationToken.None);

        Result<Guid> userId = await userResult.MatchAsync(
            user => Result.Of(user.Id).AsTask(),
            async notFound =>
            {
                return await _mediator.Send(new UserCreateCommand
                {
                    IsActive = true,
                    Name = user.FindFirst(ClaimTypes.Name)!.Value,
                    Email = email,
                    NormalizedEmail = normalizedEmail,
                }, CancellationToken.None);
            }
        );

        var principal = new ClaimsPrincipal(user);
        userId.Then(id =>
        {
            var identityWithUserId = new ClaimsIdentity([
                new Claim("id", $"{id}"),
                new Claim("sub", $"{id}"),
                new Claim("NormalizedEmail", normalizedEmail),
            ]);

            principal.AddIdentity(identityWithUserId);
        });

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return !string.IsNullOrEmpty(redirect) && redirect.StartsWith(allowedOrigin)
            ? Redirect(redirect)
            : (IActionResult)Redirect(allowedOrigin);
    }
}
