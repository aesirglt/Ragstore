namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

/// <summary>
/// 
/// </summary>
/// <returns></returns>
[Route("auth")]
public class AuthController : Controller
{
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
    public IActionResult LoginWithGoogle()
    {
        var redirectUrl = Url.Action(nameof(ResponseLogin));
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
    public async Task<IActionResult> ResponseLogin()
    {
        // Autentica o usuário e obtém os claims
        var authenticateResult = await HttpContext.AuthenticateAsync();

        if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            return BadRequest("Erro ao autenticar com o Google.");

        var user = authenticateResult.Principal;
        //var email = user.FindFirst(ClaimTypes.Email)?.Value;
        //var name = user.FindFirst(ClaimTypes.Name)?.Value;
        //var existingUser = await _userService.FindByEmailAsync(email);
        //if (existingUser == null)
        //{
        //    var newUser = new ApplicationUser
        //    {
        //        Email = email,
        //        UserName = email,
        //        Name = name
        //    };
        //    await _userService.CreateAsync(newUser);
        //}

        // Agora, você adiciona o cookie de autenticação (feito automaticamente pelo middleware)
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(user));
        return Redirect("/profile");
    }
}
