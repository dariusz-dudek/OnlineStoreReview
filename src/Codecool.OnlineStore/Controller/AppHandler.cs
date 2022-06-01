namespace Codecool.OnlineStore.Controller
{
    public class AppHandler
    {
        private BaseHandler _handler;
        private IFactory<UnitOfWork> _unitOfWorkFactory;
        private readonly ICredentialHandler _credentialHandler;
        private readonly IInputSystem _input;
        private readonly IMenuDisplay _display;
        private readonly IView _view;

        public AppHandler(IFactory<UnitOfWork> unitOfWorkFactory, IInputSystem input, IMenuDisplay display, ICredentialHandler credentiaHandler, IView view)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _input = input;
            _display = display;
            _credentialHandler = credentiaHandler;
            _view = view;
        }

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                _display.CleanScreen();
                _display.PrintMessage("Welcome to Shopi - pi - pi. Do you want to [R]egister, [L]ogin or [E]xit?");
                SelectOption(ref isRunning);
            }                     
        }

        private void SelectOption(ref bool isRunning)
        {
            if (_handler is not null)
                _handler = null;

            bool validSelection = false;
            while (!validSelection)
            {
                var userInput = _input.FetchStringValue("Enter the selected option:").ToLower();

                switch (userInput)
                {
                    case "r":
                    case "register":
                        int userId = _credentialHandler.Register();
                        validSelection = true;
                        _handler = GetNewCustomerHandler(userId);
                        _display.CleanScreen();
                        _handler.Run();
                        break;
                    case "l":
                    case "login":
                        AssignUserBasedOnCredential();
                        _handler.Run();
                        validSelection = true;
                        break;
                    case "e":
                    case "exit":
                        validSelection = true;
                        isRunning = false;
                        break;
                    default:
                        _display.PrintMessage("Incorrect option. Try again.");
                        break;
                }
            }
        }

        private void AssignUserBasedOnCredential()
        {
            User loggedUser;
            var loggedUserId = _credentialHandler.Login();

            using (var unitOfWork = _unitOfWorkFactory.GetNew()) {
                loggedUser = unitOfWork.Users.GetFirstOrDefault(u => u.UserId == loggedUserId);
            }

            switch (loggedUser.AccessLevel)
            {
                case AccessLevel.Admin:
                    _handler = GetNewAdminHandler();
                    break;
                case AccessLevel.Customer:
                    _handler = GetNewCustomerHandler(loggedUserId);
                    break;
            }
        }

        private AdminHandler GetNewAdminHandler()
        {
            return new AdminHandler(_unitOfWorkFactory, _display, _input,
                new ProductHandler(_unitOfWorkFactory, _input, _display),
                new CategoryHandler(_unitOfWorkFactory, _input, _display));
        }

        private CustomerHandler GetNewCustomerHandler(int userId)
        {
            return new CustomerHandler(_unitOfWorkFactory, _input, _view, _display, userId,
                new ProductHandler(_unitOfWorkFactory, _input, _display));
        }
    }
}
