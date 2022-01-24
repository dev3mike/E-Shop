using EShop.Inventory.API.Entities;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace EShop.Inventory.API.Data.Seeds
{
    public class InventoryContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            var isProductsCollectionEmpty = !productCollection.Find(x => true).Any();

            if (isProductsCollectionEmpty)
                productCollection.InsertMany(GetSampleProducts());
        }

        private static IEnumerable<Product> GetSampleProducts()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Seeds/SampleProducts.json");

            List<Product> products;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                products = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
            }
            return products;
        }
    }
}
