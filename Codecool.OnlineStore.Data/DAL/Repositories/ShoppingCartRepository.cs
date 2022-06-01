namespace Codecool.OnlineStore.Data.DAL.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(StoreContext context)
            : base(context) {}

        public ShoppingCart GetFirstOrDefaultWithProducts(Func<ShoppingCart, bool> condition)
        {
            return StoreContext.ShoppingCarts
                .Include(x => x.ProductList)
                .Where(condition)
                .FirstOrDefault();
        }

        public int GetProductQuantity(Product product, ShoppingCart shoppingCart)
        {
            return StoreContext.ProductShoppingCarts
                .AsNoTracking()
                .Where(x => x.ShoppingCartId.Equals(shoppingCart.ShoppingCartId) && x.ProductId.Equals(product.ProductId))
                .FirstOrDefault()
                .ProductQuantity;
        }

        public void SetProductQuantity(Product product, ShoppingCart shoppingCart, int quantity)
        {
            StoreContext.ProductShoppingCarts
                .Where(x => x.ShoppingCartId.Equals(shoppingCart.ShoppingCartId) && x.ProductId.Equals(product.ProductId))
                .FirstOrDefault()
                .ProductQuantity = quantity;
        }
    }
}
