using EShop.Inventory.API.Entities;

namespace EShop.Inventory.API.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Product>> GetProducts(int page, int pageSize);
        Task<IEnumerable<Product>> GetProductsByName(string productName, int page, int pageSize);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName, int page, int pageSize);

        Task<Product> GetProduct(string id);

        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);

        Task<bool> DeleteProduct(Product product);
        Task<bool> DeleteProduct(List<Product> products);
    }
}
