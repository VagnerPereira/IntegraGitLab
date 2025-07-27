using AppDeCastracao.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDeCastracao.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Gato> Gatos { get; set; }
        public DbSet<Dono> Donos { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<GatoStatus> GatoStatus { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gato>()
                .HasIndex(g => g.EtiquetaIdentificacao)
                .IsUnique();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GatoStatus>()
                .HasKey(gs => new { gs.GatoId, gs.StatusId });

            modelBuilder.Entity<GatoStatus>()
                .HasOne(gs => gs.Gato)
                .WithMany(g => g.GatoStatuses)
                .HasForeignKey(gs => gs.GatoId);

            modelBuilder.Entity<GatoStatus>()
                .HasOne(gs => gs.Status)
                .WithMany(s => s.GatoStatuses)
                .HasForeignKey(gs => gs.StatusId);
        }
    }
}