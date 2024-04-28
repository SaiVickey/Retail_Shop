using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineRetailShop.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrderId { get; set; }

        [BsonElement("productId")]
        public int ProductId { get; set; }

        [BsonElement("productName")] 
        public string? ProductName { get; set; }

        [BsonElement("quantity")] 
        public int Quantity { get; set; }

        [BsonElement("isOrderActive")] 
        public bool IsOrderActive { get; set; }
    }
}