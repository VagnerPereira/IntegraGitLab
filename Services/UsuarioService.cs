using AppDeCastracao.Db;
using AppDeCastracao.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace AppDeCastracao.Services
{
    public class UsuarioService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public UsuarioService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Usuario?> AutenticarAsync(string email, string senha)
        {
            await using var context = _dbContextFactory.CreateDbContext();

            var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario is null) return null;

            var senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash);
            return senhaValida ? usuario : null;
        }

        public async Task<bool> CriarUsuarioAsync(string email, string senha, string nome, string role)
        {
            await using var context = _dbContextFactory.CreateDbContext();

            if (await context.Usuarios.AnyAsync(u => u.Email == email)) return false;

            var novoUsuario = new Usuario
            {
                Nome = nome,
                Email = email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha),
                Role = role
            };

            context.Usuarios.Add(novoUsuario);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
