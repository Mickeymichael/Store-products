using Session4.Models;

namespace Session4.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly CLSDBContext context;

        public ProductRepository(CLSDBContext context)
        {
            this.context = context;
        }



        public Product Add(Product product)
        {
           context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
           var products = context.Products;
            return products;
        }

        public Product GetById(int id)
        {
            var product = context.Products.Find(id);
            return product;
        }

        public Product Update(Product product)
        {
            var prod = context.Products.Attach(product);
            prod.State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return product;
        }
    }
}
