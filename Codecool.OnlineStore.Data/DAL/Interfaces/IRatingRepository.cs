namespace Codecool.OnlineStore.Data.DAL.Interfaces
{
    public interface IRatingRepository : IRepository<Rating>
    {
        bool IsRatingAlreadyExist(Func<Rating, bool> condition);
        Rating GetFirstOrDefaultWithCustomers(Func<Rating, bool> condition);
    }
}
