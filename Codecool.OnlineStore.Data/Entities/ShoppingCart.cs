namespace Codecool.OnlineStore.Data.Entities
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public ICollection<Product> ProductList { get; set; }

        public ShoppingCart()
        {
            ProductList = new List<Product>();
        }
    }
}
