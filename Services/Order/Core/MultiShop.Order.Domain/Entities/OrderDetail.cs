using System.Text.Json.Serialization;

namespace MultiShop.Order.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductTotalPrice { get; set; }
        public int OrderingId { get; set; }

        [JsonIgnore]
        public Ordering? Ordering { get; set; }
    }
}
