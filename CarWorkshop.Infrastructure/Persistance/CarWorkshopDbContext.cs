using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistance
{
    public class CarWorkshopDbContext : DbContext
    {
        public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) : base(options) 
        { 

        }
        public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; } //podana nazwa klasy ze "ścieżką" - jest to wlasciwosc prezentujaca tabele

        protected override void OnModelCreating(ModelBuilder modelBuilder) //przekazujemy ze ContactDetails nie jest osobna tabela tylko wlasciwoscia
        {
            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .OwnsOne(c => c.ContactDetails);
        }
    }
}
