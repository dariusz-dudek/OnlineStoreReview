namespace Codecool.OnlineStore.Factories
{
    public class UserFactory : IFactory<User>
    {
        private readonly IInputSystem _input;
        private readonly IMenuDisplay _display;

        public UserFactory(IInputSystem input, IMenuDisplay display)
        {
            _input = input;
            _display = display;
        }
        public User GetNew()
        {
            var firstName = _input.FetchStringValue("Enter the first name");
            var lastName = _input.FetchStringValue("Enter the last name");
            ShoppingCart cart = new();

            return new Customer { FirstName = firstName, LastName = lastName, AccessLevel = AccessLevel.Customer, ShoppingCart = cart };
        }
    }
}

