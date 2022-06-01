namespace Codecool.OnlineStore.Data.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(StoreContext context)
            : base(context) { }

        public User GetFirstOrDefaultWithCredentials(Func<User, bool> condition)
        {
            var users = StoreContext.Users.Include(x => x.Credentials).AsNoTracking().ToList();
            users.ForEach(x => x.Credentials.Login = Cipher.DecryptData(x.Credentials.Login));
            users.ForEach(x => x.Credentials.Password = Cipher.DecryptData(x.Credentials.Password));
            var user = users.FirstOrDefault(condition);
            return user;
        }

        public User GetFirstOrDefaultWithCredentialsAsHash(Func<User, bool> condition)
            => StoreContext.Users.AsNoTracking().Include(x => x.Credentials).Where(condition).FirstOrDefault();

        public Customer GetFirstOrDefaultWithShoppingCart(Func<User, bool> condition)
            => (Customer)StoreContext.Customers.Include(x => x.ShoppingCart).ThenInclude(x => x.ProductList).Where(condition).FirstOrDefault();

        public Customer GetFirstOrDefaultWithRatings(Func<User, bool> condition)
            => (Customer)StoreContext.Customers.Include(x => x.Ratings).Where(condition).FirstOrDefault();

        public bool IsLoginAlreadyExist(string userLogin)
        {
            var logins = StoreContext.Credentials.AsNoTracking().ToList();
            logins.ForEach(x => x.Login = Cipher.DecryptData(x.Login));
            return logins.Any(x => x.Login.Equals(userLogin));
        }
    }
}
