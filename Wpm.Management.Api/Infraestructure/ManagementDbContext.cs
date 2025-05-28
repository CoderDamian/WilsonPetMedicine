using Microsoft.EntityFrameworkCore;
using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Api.Infraestructure
{
    public class ManagementDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>().HasKey(p => p.Id);
            
            modelBuilder.Entity<Pet>()
                .Property(p => p.BreedId)
                .HasConversion(p => p.Value, p => BreedId.FromCreate(p));
                            // p.value => de donde c toma el valor para guardar en la base de datos
                            // BreedId.FromCreate(p) => como debe hacer entity framework para crear el objeto

            modelBuilder.Entity<Pet>()
                .OwnsOne(p => p.Weight);
        }
    }

    public static class ManagementDbContextExtensions
    {
        public static void EnsureDbIsCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ManagementDbContext>();
            context.Database.EnsureCreated();
            context.Database.CloseConnection();
        }
    }
}
