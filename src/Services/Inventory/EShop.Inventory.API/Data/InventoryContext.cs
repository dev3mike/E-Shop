using EShop.Inventory.API.Data.Seeds;
using EShop.Inventory.API.Entities;
using MongoDB.Driver;

namespace EShop.Inventory.API.Data
{
    public class InventoryContext : IInventoryContext
    {
        public IMongoCollection<Product> Products { get; }

        public InventoryContext(IConfiguration configuration)
        {
            var connectionString = GetValueFromConfiguration(configuration, "DatabaseSettings:ConnectionString");
            var databaseName = GetValueFromConfiguration(configuration, "DatabaseSettings:DatabaseName");
            var collectionName = GetValueFromConfiguration(configuration, "DatabaseSettings:CollectionName");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            Products = database.GetCollection<Product>(collectionName);

            InventoryContextSeed.SeedData(Products);
        }

        private string GetValueFromConfiguration(IConfiguration configuration, string key)
        {
            var value = configuration.GetValue<string>(key);

            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException($"${key} is empty or null");

            return value;
        }
    }
}
