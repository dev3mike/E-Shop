using EShop.Inventory.API.Data;
using EShop.Inventory.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EShop.Inventory.API.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IInventoryContext _inventoryContext;

        public InventoryRepository(IInventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext ?? throw new ArgumentNullException(nameof(inventoryContext));
        }

        async Task<bool> IInventoryRepository.CreateProduct(Product product)
        {
            if(string.IsNullOrEmpty(product.Name))
                throw new ArgumentNullException(nameof(product.Name));

            // Set Id and CreatedDate
            product.Id = ObjectId.GenerateNewId().ToString();
            product.CreatedDate = DateTime.UtcNow;

            await _inventoryContext.Products.InsertOneAsync(product);
            return true;
        }

        async Task<bool> IInventoryRepository.DeleteProduct(Product product)
        {
            var deleteOperationStatus = await _inventoryContext.Products.DeleteOneAsync(x => x.Id == product.Id);
            return deleteOperationStatus.IsAcknowledged == true && deleteOperationStatus.DeletedCount > 0;
        }

        async Task<bool> IInventoryRepository.DeleteProduct(List<Product> products)
        {
            if (products.Count == 0)
                return false;

            var listOfIds = products.Select(x => x.Id).ToList();
            var filterCondition = Builders<Product>.Filter.In("id", listOfIds);

            var deleteStatus = await _inventoryContext.Products.DeleteManyAsync(filterCondition);
            return deleteStatus.IsAcknowledged == true && deleteStatus.DeletedCount > 0;
        }

        async Task<Product> IInventoryRepository.GetProduct(string id)
        {
            return await _inventoryContext
                            .Products
                            .Find(x => x.Id == id)
                            .FirstOrDefaultAsync();
        }

        async Task<IEnumerable<Product>> IInventoryRepository.GetProducts(int page, int pageSize)
        {
            return await _inventoryContext
                            .Products
                            .Find(p => true)
                            .SortByDescending(p => p.CreatedDate)
                            .Skip((page - 1) * pageSize)
                            .Limit(pageSize)
                            .ToListAsync();
        }

        async Task<IEnumerable<Product>> IInventoryRepository.GetProductsByCategory(string categoryName, int page, int pageSize)
        {
            var filterDefinition = Builders<Product>.Filter.Eq(x => x.Category, categoryName);
            return await _inventoryContext
                            .Products
                            .Find(filterDefinition)
                            .SortByDescending(p => p.CreatedDate)
                            .Skip((page - 1) * pageSize)
                            .Limit(pageSize)
                            .ToListAsync();
        }

        async Task<IEnumerable<Product>> IInventoryRepository.GetProductsByName(string productName, int page, int pageSize)
        {
            var filterDefinition = Builders<Product>.Filter.Eq(x => x.Name, productName);
            return await _inventoryContext
                            .Products
                            .Find(filterDefinition)
                            .SortByDescending(p => p.CreatedDate)
                            .Skip((page - 1) * pageSize)
                            .Limit(pageSize)
                            .ToListAsync();
        }

        async Task<bool> IInventoryRepository.UpdateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Id))
                throw new ArgumentNullException(nameof(product.Id));

            var updateStatus = await _inventoryContext
                                        .Products
                                        .ReplaceOneAsync(
                                                filter: x => x.Id == product.Id,
                                                replacement: product);
            return updateStatus.IsAcknowledged == true && updateStatus.ModifiedCount > 0;
        }
    }
}
