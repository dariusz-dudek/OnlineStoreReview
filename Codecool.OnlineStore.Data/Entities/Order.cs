namespace Codecool.OnlineStore.Data.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public User AssignedUser { get; set; }
        public ICollection<OrderItem> ItemsList { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? PaidAt { get; set; }

        public Order()
        {
            ItemsList = new List<OrderItem>();
        }

        public override string ToString()
            => $"Date: {CreationDate.ToString("dd-MM-yyyy")}, Items: {ItemsList.Count}, Order Status: {OrderStatus}";
    }
}
