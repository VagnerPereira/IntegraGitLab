using AppDeCastracao.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace AppDeCastracao.Helpers
{
    public static class ClaimsHelper
    {
        public static ClaimsPrincipal CriarClaims(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role)
            };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            return new ClaimsPrincipal(identity);
        }
    }
}