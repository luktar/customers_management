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

        [HttpPost("[action]")]
        public async Task AddAsync(Customer customer)
        {
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    Context.Customer.Add(customer);
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
        public async Task UpdateAsync(Customer customer)
        {

            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    Context.Entry(customer).State = EntityState.Modified;
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