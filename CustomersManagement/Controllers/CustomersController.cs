using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomersManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private CustomersContext Context { get; set; }

        public CustomersController(CustomersContext db) => Context = db;

        public CustomerData GetCustomerData(Customer customer)
        {
            Context.Entry(customer).Collection(b => b.CustomerCity).Load();

            CustomerData result = new CustomerData
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                Telephone = customer.Telephone,
                Cities = GetCities(customer.CustomerCity)
            };

            return result;

            List<CityData> GetCities(ICollection<CustomerCity> customerCities)
            {
                List<CityData> cities = new List<CityData>();
                customerCities.ToList().ForEach(x => Context.Entry(x).Reference(b => b.City).Load());
                customerCities.ToList().ForEach(x => cities.Add(new CityData { Name = x.City.Name, Id = x.CityId }));
                return cities;
            }
        }

        [HttpGet("[action]")]
        public async Task<List<Customer>> GetAllAsync()
        {
            try
            {
                return await Context.Customer.ToListAsync();
            }
            catch (Exception e)
            {
                //TODO: Logging
                throw;
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task DeleteAsync(int id)
        {
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {

                    Customer customer = await Context.Customer.FindAsync(id);

                    Context.Customer.Remove(customer);
                    await Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    // TODO: Logging
                    throw;
                }
            }
        }

        [HttpPost("[action]")]
        public async Task AddAsync(CustomerData customerData)
        {
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    Customer customer = new Customer();
                    customer.Name = customerData.Name;
                    customer.Surname = customerData.Surname;
                    customer.Email = customerData.Email;
                    customer.Telephone = customerData.Telephone;

                    Context.Customer.Add(customer);
                    await Context.SaveChangesAsync();

                    customerData.Cities.ForEach(x =>
                    {
                        customer.CustomerCity.Add(new CustomerCity
                        {
                            CityId = x.Id,
                            Customer = customer
                        });
                    });

                    await Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    // TODO: Logging
                    throw;
                }
            }
        }

        [HttpPut("[action]")]
        public async Task UpdateAsync(CustomerData customer)
        {
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    Customer dbCustomer = Context.Customer.Find(customer.Id);
                    Context.Entry(dbCustomer).Collection(b => b.CustomerCity).Load();
                    
                    foreach(CityData cityData in customer.Cities)
                    {
                        if (!dbCustomer.CustomerCity.Any(x => x.Id == cityData.Id))
                        {
                            Context.CustomerCity.Add(
                                new CustomerCity { CityId = cityData.Id, CustomerId = dbCustomer.Id });
                        }
                    }

                    dbCustomer.CustomerCity.ToList().ForEach(x =>
                    {
                        if (!customer.Cities.Any(y => y.Id == x.CityId))
                        {
                            Context.Remove(x);
                        }
                    });

                    await Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    // TODO: Logging
                    throw;
                }
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<CustomerData> GetAsync(int id)
        {
            try
            {
                Customer customer = await Context.Customer.FindAsync(id);
                return GetCustomerData(customer);
            } catch (Exception e)
            {
                // TODO: Logging
                throw;
            }
        }

        public class CityData
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class CustomerData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Telephone { get; set; }

            public List<CityData> Cities { get; set; }
        }
    }
}