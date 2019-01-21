using CustomersManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Db
{
    public interface IDbInitializer
    {
        void Initialize();
    }

    public class DbInitializer : IDbInitializer
    {
        private CustomersContext Context { get; set; }

        public DbInitializer(CustomersContext context)
        {
            Context = context;
        }

        public void Initialize()
        {
            Context.Database.EnsureCreated();

            if (Context.City.Any()) return;

            City warszawa = new City { Name = "Warszwa" };
            City krakow = new City { Name = "Kraków" };
            City katowice = new City { Name = "Katowice" };

            Context.City.Add(warszawa);
            Context.City.Add(krakow);
            Context.City.Add(katowice);

            Context.SaveChanges();

            if (Context.Customer.Any()) return;

            var customers = new List<Customer>
            {
                new Customer{ Name="Łukasz", Surname="Kowalski", Email = "lukasz.kowalski@company.com", Telephone="+48 444 333 111" },    
                new Customer{ Name="Bartosz", Surname="Zieliński", Email = "bartosz.zielinski@company.com", Telephone="+48 444 333 222"},
                new Customer{ Name="Szczepan", Surname="Małolepszy", Email = "szczepan.malolepszy@company.com", Telephone="+48 444 333 333"},
                new Customer{ Name="Kamila", Surname="Bystra", Email = "kamila.bystra@company.com", Telephone="+48 444 333 444"},
                new Customer{ Name="Jędrzej", Surname="Kiełbasa", Email = "jedrzej.kielbasa@company.com", Telephone="+48 444 333 555"},
                new Customer{ Name="Katarzyna", Surname="Swiergoń - Pasztet", Email = "katarzyna.swiergon.pasztet@company.com", Telephone="+48 444 333 666"},
                new Customer{ Name="Magdalena", Surname="Zarzycka", Email = "magdalena.zarzycka@company.com", Telephone="+48 444 333 777"},
                new Customer{ Name="Zbigniew", Surname="Ziobro", Email = "zbigniew.ziobro@company.com", Telephone="+48 444 333 888"},
                new Customer{ Name="Adam", Surname="Szałamacha", Email = "adam.szalamacha@company.com", Telephone="+48 444 333 999"},
                new Customer{ Name="Henryk", Surname="Sobieski", Email = "henryk.sobieski@company.com", Telephone="+48 444 333 000"},
            };

            customers.ForEach(x => Context.Customer.Add(x));

            Context.SaveChanges();

            List<CustomerCity> customerCities = new List<CustomerCity>();
            customerCities.Add(new CustomerCity { City = warszawa, Customer = customers[0] });
            customerCities.Add(new CustomerCity { City = katowice, Customer = customers[0] });
            customerCities.Add(new CustomerCity { City = warszawa, Customer = customers[1] });
            customerCities.Add(new CustomerCity { City = krakow, Customer = customers[1] });

            Context.CustomerCity.AddRange(customerCities);

            Context.SaveChanges();
        }
    }
}
