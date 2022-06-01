namespace Codecool.OnlineStore.Data.DAL.Repositories
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(DbContext context) 
            : base(context){}

        public bool IsRatingAlreadyExist(Func<Rating, bool> condition)
            => StoreContext.Ratings.Any(condition);

        public Rating GetFirstOrDefaultWithCustomers(Func<Rating, bool> condition)
            => StoreContext.Ratings.Include(x => x.Customers).Where(condition).FirstOrDefault();
    }
}
