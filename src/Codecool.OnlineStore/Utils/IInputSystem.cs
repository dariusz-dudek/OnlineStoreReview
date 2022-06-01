namespace Codecool.OnlineStore.Utils
{
    public interface IInputSystem
    {
        public bool FetchBooleanValue(string message);

        public int FetchIntValue(string message);

        public DateTime FetchDateTimeValue(string message);
        public int GetOptionChoice(List<string> options);
        public decimal FetchDecimalValue(string message);

        public string FetchStringValue(string message);
        public int GetValidDiscount();
        public T GetItemFromList<T>(List<T> entities);
        public int GetMenuChoice(List<string> options);
        public int GetNumberInBetween(int min, int max, string message);

        public void DisplayMessageAndWaitForAnyInput(string message);
        public string FetchPassword(string message);
    }
}
