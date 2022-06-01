namespace Codecool.OnlineStore.Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        public IUserRepository Users { get; }
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IOrderRepository Orders { get; }
        public IShoppingCartRepository ShoppingCarts { get; }
        public IRatingRepository Ratings { get; }

        public UnitOfWork(StoreContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Orders = new OrderRepository(_context);
            ShoppingCarts = new ShoppingCartRepository(_context);
            Ratings = new RatingRepository(_context);
        }

        public int CompleteUnit()
            => _context.SaveChanges();

        public void Dispose()
        {
            _context.ChangeTracker.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
