using EShop.Inventory.API.Entities;
using MongoDB.Driver;

namespace EShop.Inventory.API.Data
{
    public interface IInventoryContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
