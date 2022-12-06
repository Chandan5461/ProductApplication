using ProductApplication.Models.Domain;

namespace ProductApplication.Repostries
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        Product Get(int id);

        Product Add(Product product);

        Product Delete(int id);

        Product Update(int id, Product product);
        
    }
}
