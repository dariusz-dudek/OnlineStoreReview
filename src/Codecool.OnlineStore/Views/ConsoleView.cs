namespace Codecool.OnlineStore.Views
{
    class ConsoleView : IView
    {
        public void Display<T>(T entity) => Console.WriteLine(entity.ToString());
        

        public void DisplayAll<T>(List<T> entities)
        {
            var index = 1;
            entities.ForEach(e => Console.WriteLine($"[{index++}] {e}"));
        }
    }
}
