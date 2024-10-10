using Microsoft.EntityFrameworkCore;
using plano_saude_CP4.Models;

namespace plano_saude_CP4.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<PacientePlano> PacientesPlanos { get; set; }
        public DbSet<PlanoSaude> PlanosSaude { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PacientePlano>()
                .HasKey(pp => pp.Id); 

            modelBuilder.Entity<PacientePlano>()
                .HasOne(pp => pp.Paciente)
                .WithMany() 
                .HasForeignKey(pp => pp.PacienteId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<PacientePlano>()
                .HasOne(pp => pp.PlanoSaude)
                .WithMany() 
                .HasForeignKey(pp => pp.PlanoSaudeId)
                .OnDelete(DeleteBehavior.Cascade); 
        }

    }
}
