using MongoDB.Bson.Serialization.Attributes;

namespace OnlineRetailShop.Models
{
    public class Product
    {
        [BsonElement("_id")]
        public int ProductId { get; set; }

        [BsonElement("productName")]
        public string? ProductName { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }
    }
}
