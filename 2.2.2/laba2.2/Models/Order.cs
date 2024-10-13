using System.Collections.Generic;

namespace lab_2_1.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
