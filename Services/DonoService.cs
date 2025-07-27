using AppDeCastracao.Db;
using AppDeCastracao.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDeCastracao.Services
{
    public class DonoService
    {
        private readonly AppDbContext _context;

        public DonoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dono>> GetAllAsync()
        {
            return await _context.Donos.ToListAsync();
        }

        public async Task<Dono?> GetByIdAsync(int id)
        {
            return await _context.Donos.FindAsync(id);
        }

        public async Task CreateAsync(Dono dono)
        {
            _context.Donos.Add(dono);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Dono dono)
        {
            _context.Donos.Update(dono);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dono = await _context.Donos.FindAsync(id);
            if (dono != null)
            {
                _context.Donos.Remove(dono);
                await _context.SaveChangesAsync();
            }
        }
    }
}
