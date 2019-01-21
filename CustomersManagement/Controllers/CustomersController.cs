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

        [HttpGet("[action]")]
        public async Task<IEnumerable<Customer>> GetAllAsync()
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

        public void Add(Customer customer)
        {
            try
            {

            }
            catch (Exception e)
            {
                // TODO: Logging
                throw;
            }
        }

        public void Update(Customer customer)
        {
            try
            {

            }
            catch (Exception e)
            {
                // TODO: Logging
                throw;
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<Customer> GetAsync(int id)
        {
            try
            {
                return await Context.Customer.FindAsync(id);
            } catch (Exception e)
            {
                // TODO: Logging
                throw;
            }
        }
    }
}