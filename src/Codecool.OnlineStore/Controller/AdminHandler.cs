namespace Codecool.OnlineStore.Controller
{
    enum AdminOptions
    {
        Exit = -1,
        CreateNewProduct = 0,
        EditProduct = 1,
        CreateNewCategory = 2,
        EditCategory = 3,
        AddProductToCategory = 4,
        RemoveProductFromCategory = 5,
        ShowAllOngoingOrders = 6
    }

    public class AdminHandler : BaseHandler
    {
        ProductHandler _productHandler;
        CategoryHandler _categoryHandler;

        public AdminHandler(IFactory<UnitOfWork> unitOfWorkFactory, IMenuDisplay display, IInputSystem input, ProductHandler productHandler, CategoryHandler categoryHandler) : base(unitOfWorkFactory, input, new ConsoleView(), display)
        {
            _categoryHandler = categoryHandler;
            _productHandler = productHandler;
            _availableCommands = new[] { "Create new product", "Edit a product", "Create new category", "Edit category", "Add product to category", "Remove product from category", "Show all ongoing orders" };
        }

        public override void RunFeatureBasedOn(int userInput)
        {
            switch (userInput)
            {
                case (int)AdminOptions.CreateNewProduct:
                    CreateNewProduct();
                    break;
                case (int)AdminOptions.EditProduct:
                    EditProduct();
                    break;
                case (int)AdminOptions.CreateNewCategory:
                    CreateNewCategory();
                    break;
                case (int)AdminOptions.EditCategory:
                    EditCategory();
                    break;
                case (int)AdminOptions.AddProductToCategory:
                    AddProductToCategory();
                    break;
                case (int)AdminOptions.RemoveProductFromCategory:
                    RemoveProductFromCategory();
                    break;
                case (int)AdminOptions.ShowAllOngoingOrders:
                    DisplayAllOngoingOrders();
                    break;
                case (int)AdminOptions.Exit:
                    return;
                default:
                    break;
            }
        }

        private void CreateNewProduct() => _productHandler.CreateNewProduct();
        private void EditProduct() => _productHandler.EditProduct();
        private void CreateNewCategory() => _categoryHandler.CreateNewCategory();
        private void EditCategory() => _categoryHandler.EditCategory();
        private void AddProductToCategory() => _categoryHandler.AddProductToCategory();
        private void RemoveProductFromCategory() => _categoryHandler.RemoveProductFromCategory();
        private void DisplayAllOngoingOrders()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var orders = unitOfWork.Orders.GetAll().ToList();
            if (!orders.Any())
            {
                _display.PrintMessage("There are no orders");
                return;
            }
            _view.DisplayAll(orders);
        }
    }
}