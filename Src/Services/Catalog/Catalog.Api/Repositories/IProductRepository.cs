using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string Id);
        Task<IEnumerable<Product>> GetProductByName(string productName);
        Task<IEnumerable<Product>> GetProductByCategoryName(string categoryName);
        Task Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string Id); 
    }
}
