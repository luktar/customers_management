using System;
using System.Collections.Generic;

namespace CustomersManagement.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int Cityid { get; set; }

        public virtual City IdNavigation { get; set; }
    }
}
