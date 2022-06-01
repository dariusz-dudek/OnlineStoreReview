namespace Codecool.OnlineStore.Controller
{
    public abstract class BaseHandler
    {
        protected readonly IFactory<UnitOfWork> _unitOfWorkFactory;
        protected IInputSystem _inputManager;
        protected IView _view;
        protected IMenuDisplay _display;
        protected string[] _availableCommands;

        protected BaseHandler(IFactory<UnitOfWork> unitOfWorkFactory, IInputSystem inputManager, IView view, IMenuDisplay display)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _inputManager = inputManager;
            _view = view;
            _display = display;
        }

        public void Run()
        {
            int userInput = 0;

            while (true)
            {
                userInput = _inputManager.GetMenuChoice(_availableCommands.ToList());
                if (userInput == -1) break;
                RunFeatureBasedOn(userInput);
                _inputManager.DisplayMessageAndWaitForAnyInput("");
                _display.CleanScreen();
            }
        }

        public virtual void RunFeatureBasedOn(int userInput) { }
    }
}