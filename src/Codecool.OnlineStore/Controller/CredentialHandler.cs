namespace Codecool.OnlineStore.Controller
{
    public class CredentialHandler : ICredentialHandler
    {
        private readonly IFactory<User> _userFactory;
        private readonly IFactory<Credentials> _credentialFactory;
        private readonly IFactory<UnitOfWork> _unitOfWorkFactory;
        private readonly IInputSystem _input;
        private readonly IMenuDisplay _display;

        public CredentialHandler(IFactory<UnitOfWork> unitOfWorkFactory, IFactory<User> userFactory, IFactory<Credentials> credentialFactory, IInputSystem input, IMenuDisplay menuDisplay)
        {
            _userFactory = userFactory;
            _credentialFactory = credentialFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
            _input = input;
            _display = menuDisplay;
        }

        public int Register()
        {
            _display.CleanScreen();
            Credentials credentials = _credentialFactory.GetNew();
            User user = _userFactory.GetNew();
            user.Credentials = credentials;

            using var unitOfWork = _unitOfWorkFactory.GetNew();
            unitOfWork.Users.Add(user);
            unitOfWork.CompleteUnit();

            return GetRegisteredUserId(user);
        }

        private int GetRegisteredUserId(User user)
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();          
            var targetUser = unitOfWork.Users.GetFirstOrDefaultWithCredentialsAsHash(x => x.Credentials.Password == user.Credentials.Password && x.Credentials.Login == user.Credentials.Login);
            return targetUser.UserId;        
        }

        public int Login()
        {
            User loggedUser;
            _display.CleanScreen();
            while (true)
            {
                var login = _input.FetchStringValue("Enter your login");
                var password = _input.FetchPassword("Enter your password");

                using var unitOfWork = _unitOfWorkFactory.GetNew();

                loggedUser = unitOfWork.Users.GetFirstOrDefaultWithCredentials(u => u.Credentials.Login.Equals(login) && u.Credentials.Password.Equals(password));

                if (loggedUser != null) {
                    _display.CleanScreen();
                    _display.PrintMessage($"Hello, {loggedUser.FirstName} {loggedUser.LastName}.");
                    break; }
                else _display.PrintMessage("Incorrect login or password. Try again.");
            }
            return loggedUser.UserId;
        }
    }
}
