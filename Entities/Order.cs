namespace Nullean.OnlineStore.Entities
{
    public class Order
    {
        public Guid GuidId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalCount { get; set; }
    }
}
