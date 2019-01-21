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
    public class CitiesController : ControllerBase
    {
        private CustomersContext Context { get; set; }

        public CitiesController(CustomersContext db) => Context = db;

        [HttpGet("[action]")]
        public async Task<List<City>> GetAllAsync()
        {
            return await Context.City.ToListAsync();
            // TDOD: Try Catch and logging
        }
    }
}