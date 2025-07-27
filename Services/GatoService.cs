using AppDeCastracao.Entities;
using Microsoft.EntityFrameworkCore;
using AppDeCastracao.Db;

namespace AppDeCastracao.Services
{
    public class GatoService
    {
        private readonly AppDbContext _context;

        public GatoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Gato>> GetAllAsync() =>
        await _context.Gatos
        .Include(g => g.GatoStatuses)
        .ThenInclude(gs => gs.Status)
        .ToListAsync();

        public async Task CreateAsync(Gato gato)
        {
            var existe = await _context.Gatos.AnyAsync(g => g.EtiquetaIdentificacao == gato.EtiquetaIdentificacao);
            if (existe)
                throw new Exception("Já existe um gato com essa etiqueta de identificação.");
            
            _context.Gatos.Add(gato);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var gato = await _context.Gatos.FindAsync(id);
            if (gato is not null)
            {
                _context.Gatos.Remove(gato);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Gato gato)
        {
            var gatoExistente = await _context.Gatos.FindAsync(gato.Id);
            if (gatoExistente is null) return;

            // Atualiza os campos
            gatoExistente.Nome = gato.Nome;
            gatoExistente.Sexo = gato.Sexo;
            gatoExistente.IdadeAnos = gato.IdadeAnos;
            gatoExistente.IdadeMeses = gato.IdadeMeses;
            gatoExistente.Cor = gato.Cor;
            gatoExistente.Observacao = gato.Observacao;
            gatoExistente.EtiquetaIdentificacao = gato.EtiquetaIdentificacao;
            gatoExistente.Localizacao = gato.Localizacao;
            gatoExistente.FotoUrl = gato.FotoUrl;

            await _context.SaveChangesAsync();
        }
        public async Task<Gato?> GetByIdAsync(int id) =>
        await _context.Gatos
        .Include(g => g.GatoStatuses)
        .FirstOrDefaultAsync(g => g.Id == id);
    }
}
