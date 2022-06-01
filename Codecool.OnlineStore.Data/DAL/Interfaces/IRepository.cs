namespace Codecool.OnlineStore.Data.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetFirstOrDefault(Func<T, bool> condition);
        IEnumerable<T> GetAll();
    }
}
