namespace Codecool.OnlineStore.Views.Interfaces
{
    public interface IView
    {
        public void Display<T>(T entity);
        public void DisplayAll<T>(List<T> entities);
    }
}
