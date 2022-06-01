namespace Codecool.OnlineStore.Data.DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreContext context)
            : base(context) { }

        public Category GetFirstOrDefaultWithProductsList(Func<Category, bool> condition)
        {
            return StoreContext.Categories
                .Include(x => x.ProductsList)
                .Where(condition)
                .FirstOrDefault();     
        }

        public IEnumerable<Category> GetAllSortedByIsFeatured()
        {
            return StoreContext.Categories
                .AsNoTracking()
                .OrderByDescending(x => x.IsFeatured);
        }
    }
}
