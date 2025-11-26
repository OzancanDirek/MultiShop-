using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class ProductDetail
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductDetailId { get; set; }
        public string ProductDetailDescription { get; set; }
        public string ProductDetailInfo { get; set; }

        public string ProductId { get; set; }

        [BsonIgnore]
        public Product Product { get; set; }
    }
}
