namespace Codecool.OnlineStore.Data.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetFirstOrDefaultWithCredentials(Func<User, bool> condition);
        User GetFirstOrDefaultWithCredentialsAsHash(Func<User, bool> condition);
        Customer GetFirstOrDefaultWithShoppingCart(Func<User, bool> condition);
        Customer GetFirstOrDefaultWithRatings(Func<User, bool> condition);
        bool IsLoginAlreadyExist(string userLogin);
    }
}
