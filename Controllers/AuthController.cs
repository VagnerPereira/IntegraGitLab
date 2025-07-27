using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AppDeCastracao.Services;
using AppDeCastracao.Helpers;

namespace AppDeCastracao.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AuthController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string senha)
        {
            var usuario = await _usuarioService.AutenticarAsync(email, senha);
            if (usuario is null)
            {
                return Redirect("/login?erro=true");
            }

            var claimsPrincipal = ClaimsHelper.CriarClaims(usuario);
            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

            return Redirect("/");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return Redirect("/login");
        }
    }
}
