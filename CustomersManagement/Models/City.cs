using System;
using System.Collections.Generic;

namespace CustomersManagement.Models
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
