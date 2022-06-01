namespace Codecool.OnlineStore.Data.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected StoreContext StoreContext { get => _context as StoreContext; }

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
            => _context.Set<T>().Add(entity);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public IEnumerable<T> GetAll()
            => _context.Set<T>().AsNoTracking().ToList();

        public T GetFirstOrDefault(Func<T, bool> condition)
            => _context.Set<T>().AsNoTracking().Where(condition).FirstOrDefault();
    }
}
