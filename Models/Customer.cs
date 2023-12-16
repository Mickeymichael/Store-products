using System.ComponentModel.DataAnnotations;

namespace Session4.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public  string Name { get; set; }    
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string  Image { get; set; }

        public virtual List<Order> Orders { get; set; }

    }
}
