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
            List<Product> products;
            using (StreamReader r = new StreamReader("Json/SampleProducts.json"))
            {
                string json = r.ReadToEnd();
                products = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
            }
            return products;
        }
    }
}
