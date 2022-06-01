namespace Codecool.OnlineStore.Data.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context)
            : base(context) { }

        public Product GetFirstOrDefaultWithCategoriesAndCarts(Func<Product, bool> condition)
        {
            return StoreContext.Products
                .Include(x => x.CategoriesList)
                .Include(x => x.ShoppingCart)
                .Where(condition)
                .FirstOrDefault();
        }
    }
}
