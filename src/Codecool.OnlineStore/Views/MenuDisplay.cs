namespace Codecool.OnlineStore.Views
{
    public class MenuDisplay : IMenuDisplay
    {
        public void CleanScreen() => Console.Clear();
        public void PrintMessageWithoutNewLine(string message) => Console.Write(message);
        public void PrintMessage(string message) => Console.WriteLine(message);

        public void PrintOptions(List<string> options, string message)
        {
            int index = 1;
            PrintMessage($"~~~~~~ {message} ~~~~~~");
            options.ForEach(o => PrintMessage($"[{index++}]: {o}"));
            PrintMessage($"[0]: Exit");
        }

        public void PrintMenuOptions(List<string> options)
        {
            int index = 1;
            PrintMessage("~~~~~~ Options available ~~~~~~");
            options.ForEach(o => PrintMessage($"[{index++}]: {o}"));
            PrintMessage($"[0]: Logout");
        }
    }
}
