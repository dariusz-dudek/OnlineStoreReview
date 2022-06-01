namespace Codecool.OnlineStore.Data.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public override string ToString()
            => $"Name: {ProductName}, Quantity: {Quantity}";
    }
}
