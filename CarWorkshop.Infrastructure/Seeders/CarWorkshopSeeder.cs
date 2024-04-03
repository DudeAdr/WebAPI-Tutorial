using CarWorkshop.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Seeders
{
    public class CarWorkshopSeeder
    {
        private CarWorkshopDbContext _dbContext;

        public CarWorkshopSeeder(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync()) //sprawdza czy polaczenie z bd jest mozliwe
            {
                if(!_dbContext.CarWorkshops.Any()) //sprawdzi czy tabela jest pusta
                {
                    var mazdaAso = new Domain.Entities.CarWorkshop()
                    {
                        Name = "Mazda ASO",
                        Description = "Autoryzowany serwis mazda",
                        ContactDetails = new()
                        {
                            City = "Kraków",
                            Street = "Szkolna 2",
                            PostalCode = "123-33",
                            PhoneNumber = "+48228448228"
                        }
                    };
                    mazdaAso.EncodeName();
                    _dbContext.CarWorkshops.Add(mazdaAso);
                    await _dbContext.SaveChangesAsync(); 
                }
            }
        }
    }
}
