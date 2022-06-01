namespace Codecool.OnlineStore
{
    /// <summary>
    /// This is the main class of your program which contains Main method
    /// </summary>
    public class Program
    {
        /// <summary>
        /// This is the entry point of your program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            IMenuDisplay display = new MenuDisplay();
            IView view = new ConsoleView();
            IInputSystem input = new ConsoleInputSystem(display, view);
            IFactory<User> userFactory = new UserFactory(input, display);
            IFactory<Credentials> credentialFactory = new CredentialsFactory(input, display);
            ICredentialHandler credentialHandler = new CredentialHandler(new UnitOfWorkFactory(new StoreContext()), userFactory, credentialFactory, input, display);

            AppHandler app = new(new UnitOfWorkFactory(new StoreContext()), input, display, credentialHandler, view);
            app.Run();
        }
    }
}
