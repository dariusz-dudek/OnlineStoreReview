namespace Codecool.OnlineStore.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime DiscountExpiration { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Category> CategoriesList { get; set; }
        public ICollection<ShoppingCart> ShoppingCart { get; set; }

        public Product()
        {
            CategoriesList = new List<Category>();
            ShoppingCart = new List<ShoppingCart>();
        }

        public override string ToString()
        {
            var product = new StringBuilder($"Name: {ProductName}, Price: {GetCurrentPrice():C2}");
            if (DiscountExpiration >= DateTime.Now)
                product.Append($" (includes {Discount:P0} discount, special offer valid until: {DiscountExpiration:yyyy-MM-dd hh:mm})");

            return product.ToString();
        }

        public decimal GetCurrentPrice()
        {
            if (DiscountExpiration >= DateTime.Now)
                return Price * (1 - Discount);
            else return Price;
        }
    }
}
