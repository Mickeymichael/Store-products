using Microsoft.EntityFrameworkCore;

namespace Session4.Models
{
    public class CLSDBContext:DbContext
    {
        public CLSDBContext(DbContextOptions<CLSDBContext> option):base(option) 
        {
                
        }


        public DbSet<Customer> Customers { get; set; }
         public DbSet <Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
