namespace Codecool.OnlineStore.Controller
{
    enum EditOptions
    {
        Exit = -1,
        EditName = 0,
        EditDescription = 1,
        EditPrice = 2,
        EditAmount = 3,
        AddDiscount = 4,
        Deactivate = 5,
        Activate = 6
    }
    public class ProductHandler : BaseHandler, IProductHandler
    {
        public ProductHandler(IFactory<UnitOfWork> unitOfWorkFactory, IInputSystem inputManager, IMenuDisplay display) : base(unitOfWorkFactory, inputManager, new ConsoleView(), display)
        {
            _availableCommands = new[] { "Edit name", "Edit description", "Edit price", "Edit amount", "Add discount", "Deactivate product", "Activate product" };
        }

        public override void RunFeatureBasedOn(int userInput)
        {

        }

        public void CreateNewProduct()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var newProduct = GenerateNewProduct();
            unitOfWork.Products.Add(newProduct);
            unitOfWork.CompleteUnit();
        }

        private Product GenerateNewProduct()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();

            var name = CheckIfNameIsExist(unitOfWork);
            var description = _inputManager.FetchStringValue("Provide description");
            var price = _inputManager.FetchDecimalValue("Provide price");
            var amount = _inputManager.FetchIntValue("Provide amount");
            var newProduct = new Product() { ProductName = name, Description = description, Price = price, Amount = amount, Discount = 0, IsAvailable = true };
            return newProduct;
        }

        private string CheckIfNameIsExist(UnitOfWork unitOfWork)
        {
            //  while()
            var name = _inputManager.FetchStringValue("Provide name");
            if (unitOfWork.Products.GetAll().Any(p => p.ProductName.Equals(name.ToLower())))
            {
                _display.PrintMessage("Product with with this name already exist");

            }
            return name;
        }

        public void EditProduct()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var products = unitOfWork.Products.GetAll().ToList();
            if (!products.Any())
            {
                _display.PrintMessage("There are no products to edit");
                return;
            }
            var product = _inputManager.GetItemFromList(products);
            var productToEdit = unitOfWork.Products.GetFirstOrDefaultWithCategoriesAndCarts(p => p.ProductId == product.ProductId);
            while (true)
            {
                int choice = _inputManager.GetOptionChoice(_availableCommands.ToList());
                switch (choice)
                {
                    case (int)EditOptions.EditName:
                        EditName(productToEdit);
                        break;
                    case (int)EditOptions.EditDescription:
                        EditDescription(productToEdit);
                        break;
                    case (int)EditOptions.EditPrice:
                        EditPrice(productToEdit);
                        break;
                    case (int)EditOptions.EditAmount:
                        EditAmount(productToEdit);
                        break;
                    case (int)EditOptions.AddDiscount:
                        AddDiscount(productToEdit);
                        break;
                    case (int)EditOptions.Deactivate:
                        ChangeActiveState(productToEdit, false);
                        break;
                    case (int)EditOptions.Activate:
                        ChangeActiveState(productToEdit, true);
                        break;
                    case (int)EditOptions.Exit:
                        return;
                    default:
                        break;
                }
                _display.CleanScreen();
                unitOfWork.Products.Update(productToEdit);
                unitOfWork.CompleteUnit();
            }

        }

        private void AddDiscount(Product productToEdit)
        {
            var discountAsInt = _inputManager.GetValidDiscount();

            decimal discount = (decimal)discountAsInt / 100;
            var discountDate = _inputManager.FetchDateTimeValue("Provide the date and time (discount will be valid until this time)");

            productToEdit.Discount = discount;
            productToEdit.DiscountExpiration = discountDate;

            _inputManager.DisplayMessageAndWaitForAnyInput("Discount added.");
        }

        public void EditName(Product product)
        {
            var newName = _inputManager.FetchStringValue("Provide new name");
            product.ProductName = newName;
        }

        public void EditDescription(Product product)
        {
            var newDescription = _inputManager.FetchStringValue("Provide new description");
            product.Description = newDescription;
        }

        public void EditPrice(Product product)
        {
            var newPrice = _inputManager.FetchDecimalValue("Provide new price");
            product.Price = newPrice;
        }
        public void EditAmount(Product product)
        {
            var newAmount = _inputManager.FetchIntValue("Provide new amount");
            product.Amount = newAmount;
        }

        public void ChangeActiveState(Product product, bool state)
        {
            product.IsAvailable = state;
            if (state == true)
            {
                _display.PrintMessage($"{product.ProductName} is active");
                _inputManager.DisplayMessageAndWaitForAnyInput("");
                return;
            }
            _display.PrintMessage($"{product.ProductName} is deactivated");
            _inputManager.DisplayMessageAndWaitForAnyInput("");

        }

        public List<Product> GetAllProductsFromSpecifiedCategory()
        {
            List<Product> productsFromCategory = new();

            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var allCategories = unitOfWork.Categories.GetAllSortedByIsFeatured().ToList();

            if (!allCategories.Any())
            {
                _display.PrintMessage("You don't have any categories.");
                return productsFromCategory;
            }

            var pickedCategory = _inputManager.GetItemFromList(allCategories);
            productsFromCategory = unitOfWork.Categories.GetFirstOrDefaultWithProductsList(c => c.CategoryId == pickedCategory.CategoryId).ProductsList.ToList();
            unitOfWork.CompleteUnit();
            return productsFromCategory;
        }
    }
}