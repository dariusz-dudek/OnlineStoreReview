namespace Codecool.OnlineStore.Data.Entities
{
    public class ProductShoppingCart
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
