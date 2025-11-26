using System.Text.Json.Serialization;

namespace MultiShop.Order.Domain.Entities
{
    public class Ordering
    {
        public int OrderingId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        [JsonIgnore]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
