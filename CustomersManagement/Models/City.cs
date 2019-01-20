using System;
using System.Collections.Generic;

namespace CustomersManagement.Models
{
    public partial class City
    {
        public City()
        {
            CustomerCity = new HashSet<CustomerCity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerCity> CustomerCity { get; set; }
    }
}
