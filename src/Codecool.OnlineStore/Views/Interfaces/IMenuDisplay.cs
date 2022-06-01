namespace Codecool.OnlineStore.Views.Interfaces
{
    public interface IMenuDisplay
    {
        public void PrintMessageWithoutNewLine(string message);
        public void PrintMessage(string message);
        public void CleanScreen();
        public void PrintOptions(List<string> options, string message);
        public void PrintMenuOptions(List<string> options);
    }
}
