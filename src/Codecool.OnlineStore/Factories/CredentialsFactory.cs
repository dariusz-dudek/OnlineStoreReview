

namespace Codecool.OnlineStore.Factories
{
    public class CredentialsFactory : IFactory<Credentials>
    {
        private readonly IInputSystem _input;
        private readonly IMenuDisplay _display;

        public CredentialsFactory(IInputSystem input, IMenuDisplay display)
        {
            _input = input;
            _display = display;
        }

        public Credentials GetNew()
        {
            var login = Cipher.EncryptData(GetValidLogin());
            var email = Cipher.EncryptData(_input.FetchStringValue("Enter email"));
            var password = Cipher.EncryptData(_input.FetchPassword("Enter password"));

            return new Credentials { Login = login, Email = email, Password = password };
        }

        public string GetValidLogin()
        {
            var validLogin = false;
            var login = string.Empty;

            while (!validLogin)
            {
                login = _input.FetchStringValue("Enter login");

                using var unitOfWork = new UnitOfWork(new StoreContext());
                if (unitOfWork.Users.IsLoginAlreadyExist(login))
                    _display.PrintMessage("This login already exists. Choose another one.");
                else if (String.IsNullOrEmpty(login))
                    _display.PrintMessage("Empty login is not allowed.");
                else validLogin = true;
            }
            return login;
        }
    }
}
