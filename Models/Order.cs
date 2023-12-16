using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session4.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }  
    }
}
