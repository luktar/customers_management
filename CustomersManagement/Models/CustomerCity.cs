using System;
using System.Collections.Generic;

namespace CustomersManagement.Models
{
    public partial class CustomerCity
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int CustomerId { get; set; }

        public virtual City City { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
