using System.Collections.Generic;

namespace lab_2_1.Models
{
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
