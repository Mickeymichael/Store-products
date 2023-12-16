using Session4.Models;
namespace Session4.Repository
{
    public interface IProduct
    {
        IEnumerable<Product> GetAllProducts();
        Product GetById(int id);
        Product Add(Product product);
        Product Update(Product product);
        Product Delete(Product product);
    }
}
