using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistance
{
    public class CarWorkshopDbContext : IdentityDbContext
    {
        public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) : base(options) 
        { 

        }
        public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; } //podana nazwa klasy ze "ścieżką" - jest to wlasciwosc prezentujaca tabele
        public DbSet<Domain.Entities.CarWorkshopService> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //przekazujemy z ContactDetails nie jest osobna tabela tylko wlasciwoscia
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .OwnsOne(c => c.ContactDetails);

            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .HasMany(c => c.Services)
                .WithOne(s => s.CarWorkshop)
                .HasForeignKey(s => s.CarWorkshopId);
        }
    }
}
