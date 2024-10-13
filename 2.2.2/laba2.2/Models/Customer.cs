using System.Collections.Generic;

namespace lab_2_1.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
