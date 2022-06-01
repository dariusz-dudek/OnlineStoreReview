namespace Codecool.OnlineStore.Data.DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetFirstOrDefaultWithProductsList(Func<Category, bool> condition);
        IEnumerable<Category> GetAllSortedByIsFeatured();
    }
}
