namespace Codecool.OnlineStore.Data.DAL.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        ShoppingCart GetFirstOrDefaultWithProducts(Func<ShoppingCart, bool> condition);
        int GetProductQuantity(Product product, ShoppingCart shoppingCart);
        void SetProductQuantity(Product product, ShoppingCart shoppingCart, int quantity);
    }
}
