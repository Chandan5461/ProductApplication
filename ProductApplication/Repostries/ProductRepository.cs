using ProductApplication.Data;
using ProductApplication.Models.Domain;

namespace ProductApplication.Repostries
{
    public class ProductRepository : IProductRepository
    {
        private readonly PUserDbContext pUserDbContext;

        public ProductRepository(PUserDbContext pUserDbContext)
        {
            this.pUserDbContext = pUserDbContext;
        }

        public Product Add(Product product)
        {
            product.Id = 0;
            pUserDbContext.Add(product);
            pUserDbContext.SaveChanges();
            return product;
        }

        public Product Delete(int id)
        {
            var product = pUserDbContext.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return null;
            }

            pUserDbContext.Products.Remove(product);
            pUserDbContext.SaveChanges();
            return product;


        }



        public List<Product> GetAll()
        {
            return pUserDbContext.Products.ToList();
        }

        public Product Get(int id)
        {
            return pUserDbContext.Products.FirstOrDefault(x => x.Id == id);

        }

        public Product Update(int id, Product product)
        {

            var existingproduct = pUserDbContext.Products.FirstOrDefault(x => x.Id == id);

            if (existingproduct == null)
            {
                return null;
            }
            existingproduct.Name = product.Name;
            existingproduct.quantity = product.quantity;
            existingproduct.Price = product.Price;
            existingproduct.status = product.status;

            pUserDbContext.SaveChanges();

            return existingproduct;

        }

        
    }
}
