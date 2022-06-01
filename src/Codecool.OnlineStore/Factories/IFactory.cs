namespace Codecool.OnlineStore.Factories
{
    public interface IFactory<T>
    {
        public T GetNew();
    }
}
