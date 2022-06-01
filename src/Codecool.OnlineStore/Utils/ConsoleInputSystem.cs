namespace Codecool.OnlineStore.Utils
{
    public class ConsoleInputSystem : IInputSystem
    {
        private const string TrueBooleanInput = "yes";

        private readonly IMenuDisplay _menuDisplay;
        private readonly IView _view;

        public ConsoleInputSystem(IMenuDisplay menuDisplay, IView viewDisplay)
        {
            _menuDisplay = menuDisplay;
            _view = viewDisplay;
        }

        public bool FetchBooleanValue(string message)
        {
            _menuDisplay.PrintMessage(message);
            return Console.ReadLine().ToLower().Equals(TrueBooleanInput);
        }

        public int FetchIntValue(string message)
        {
            _menuDisplay.PrintMessage(message);
            int outcome = 0;
            bool notValid = true;
            while (notValid)
            {
                var userInput = Console.ReadLine();
                if (userInput != null && Int32.TryParse(userInput, out outcome))
                {
                    return outcome;
                }
                else
                {
                    _menuDisplay.PrintMessage("Provided input must be of the int type");
                }
            }
            return outcome;
        }

        public DateTime FetchDateTimeValue(string message)
        {
            DateTime dateTime;
            _menuDisplay.PrintMessage(message);

            while (true)
            {

                var year = FetchIntValue("Enter the year.");
                var month = FetchIntValue("Enter the month.");
                var day = FetchIntValue("Enter the day.");
                var hour = FetchIntValue("Enter the hour.").ToString().PadLeft(2, '0');
                var minute = FetchIntValue("Enter the minute.").ToString().PadLeft(2, '0');

                var dateTimeString = year + "-" + month + "-" + day + "T" + hour + ":" + minute;

                if (!DateTime.TryParse(dateTimeString, out dateTime))
                {
                    _menuDisplay.PrintMessage("Incorrect date and time. Try again.");
                }
                else break;
            }

            return dateTime;
        }

        public decimal FetchDecimalValue(string message)
        {
            _menuDisplay.PrintMessage(message);
            bool notValid = true;
            decimal outcome = 0;
            while (notValid)
            {
                var userInput = Console.ReadLine();
                if (userInput != null && decimal.TryParse(userInput, out outcome))
                {
                    return outcome;
                }
                else
                {
                    _menuDisplay.PrintMessage("Provided input must be of the decimal type");
                }
            }
            return outcome;
        }

        public string FetchStringValue(string message)
        {
            _menuDisplay.PrintMessage(message);
            string outcome = string.Empty;
            bool notValid = true;
            while (notValid)
            {
                var userInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userInput))
                {
                    outcome = userInput;
                    return outcome;
                }
                else
                {
                    _menuDisplay.PrintMessage("Provided input can not be empty");
                }
            }
            return outcome;
        }
        public int GetValidDiscount()
        {
            bool isValidPercentage = false;
            int discountAsInt = 0;
            int minValidDiscount = 1;
            int maxValidDiscount = 99;

            while (!isValidPercentage)
            {
                discountAsInt = FetchIntValue("Provide the discount percentage");
                if (discountAsInt >= minValidDiscount && discountAsInt <= maxValidDiscount)
                    isValidPercentage = true;
                else
                    _menuDisplay.PrintMessage("Incorrect percentage. Try again.");
            }
            return discountAsInt;
        }

        public T GetItemFromList<T>(List<T> entities)
        {
            _view.DisplayAll(entities);
            int input = GetNumberInBetween(1, entities.Count, $"Provide index 1 - {entities.Count}");
            return entities[input - 1];
        }


        public int GetMenuChoice(List<string> options)
        {
            _menuDisplay.PrintMenuOptions(options);
            int input = GetNumberInBetween(0, options.Count, $"Provide index 0 - {options.Count}");
            return input - 1;
        }
        public int GetOptionChoice(List<string> options)
        {
            _menuDisplay.PrintOptions(options, "Options available");
            int input = GetNumberInBetween(0, options.Count, $"Provide index 0 - {options.Count}");
            return input - 1;
        }
        public int GetNumberInBetween(int min, int max, string message)
        {
            bool notValid = true;
            string messageToDisplay = message;
            while (notValid)
            {
                int input = FetchIntValue(messageToDisplay);
                messageToDisplay = "";
                if (input >= min && input <= max)
                {
                    return input;
                }
                else
                {
                    _menuDisplay.PrintMessage($"Provided int can't be smaller than {min} or bigger than {max}");
                }
            }
            return 0;
        }

        public void DisplayMessageAndWaitForAnyInput(string message)
        {
            _menuDisplay.PrintMessage($"{message} Press Enter to continue...");
            Console.ReadKey();
        }

        public string FetchPassword(string message)
        {
            StringBuilder password = new();
            ConsoleKeyInfo consoleKey;

            _menuDisplay.PrintMessage(message);

            do {
                consoleKey = Console.ReadKey(intercept: true);
                if (consoleKey.Key == ConsoleKey.Backspace && password.Length > 0) {
                    password.Remove(password.Length - 1, 1);
                    _menuDisplay.PrintMessageWithoutNewLine("\b \b"); }
                else if (!char.IsControl(consoleKey.KeyChar)) {
                    password.Append(consoleKey.KeyChar);
                    _menuDisplay.PrintMessageWithoutNewLine("*"); }
            } while (consoleKey.Key != ConsoleKey.Enter);

            _menuDisplay.PrintMessage(string.Empty);
            return password.ToString();
        }
    }
}
