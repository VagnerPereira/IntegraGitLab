using AppDeCastracao.Db;
using Microsoft.EntityFrameworkCore;
using AppDeCastracao.Entities;

namespace AppDeCastracao.Services
{
    public class StatusService
    {
        private readonly AppDbContext _context;

        public StatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Status>> GetAllAsync()
        {
            return await _context.Status.ToListAsync();
        }
    }
}