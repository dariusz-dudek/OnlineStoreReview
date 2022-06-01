namespace Codecool.OnlineStore.Data.DAL.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetFirstOrDefaultWithCategoriesAndCarts(Func<Product, bool> condition);
    }
}
