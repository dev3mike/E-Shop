using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EShop.Inventory.API.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreatedDate = DateTime.UtcNow;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}
