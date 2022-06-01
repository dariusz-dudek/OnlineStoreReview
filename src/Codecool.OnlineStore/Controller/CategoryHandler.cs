namespace Codecool.OnlineStore.Controller
{
    public class CategoryHandler : BaseHandler
    {
        public CategoryHandler(IFactory<UnitOfWork> unitOfWorkFactory, IInputSystem inputManager, IMenuDisplay display) : base(unitOfWorkFactory, inputManager, new ConsoleView(), display) {}

        public void CreateNewCategory()
        {
            var categoryName = _inputManager.FetchStringValue("Provide category name: ");
            var isFeaturedCategory = _inputManager.FetchBooleanValue("Type yes if this category should be featured: ");
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            unitOfWork.Categories.Add(new Category { CategoryName = categoryName, IsFeatured = isFeaturedCategory });
            unitOfWork.CompleteUnit();
        }

        public void EditCategory()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var allCategories = unitOfWork.Categories.GetAll().ToList();
            if (!allCategories.Any())
            {
                _display.PrintMessage("There are no categories to edit");
                return;
            }
            var pickedCategory = _inputManager.GetItemFromList(allCategories);
            var category = unitOfWork.Categories.GetFirstOrDefaultWithProductsList(x => x.CategoryId == pickedCategory.CategoryId);
            category.CategoryName = _inputManager.FetchStringValue($"Provide new category name for {category.CategoryName}: ");
            category.IsFeatured = _inputManager.FetchBooleanValue("Type yes if this category should be featured: ");
            unitOfWork.CompleteUnit();
        }

        public void AddProductToCategory()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var allCategories = unitOfWork.Categories.GetAll().ToList();
            var pickedCategory = _inputManager.GetItemFromList(allCategories);
            var allProducts = unitOfWork.Products.GetAll().ToList();
            var pickedProduct = _inputManager.GetItemFromList(allProducts);
            var category = unitOfWork.Categories.GetFirstOrDefaultWithProductsList(x => x.CategoryId == pickedCategory.CategoryId);
            var product = unitOfWork.Products.GetFirstOrDefaultWithCategoriesAndCarts(x => x.ProductId == pickedProduct.ProductId);
            category.ProductsList.Add(product);
            unitOfWork.CompleteUnit();
        }

        public void RemoveProductFromCategory()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var allCategories = unitOfWork.Categories.GetAll().ToList();
            if (!allCategories.Any())
            {
                _display.PrintMessage("There are no categories");
                return;
            }
            var pickedCategory = _inputManager.GetItemFromList(allCategories);
            var category = unitOfWork.Categories.GetFirstOrDefaultWithProductsList(x => x.CategoryId == pickedCategory.CategoryId);
            var products = category.ProductsList.ToList();
            if (!products.Any())
            {
                _display.PrintMessage("There are no products in picked category");
                return;
            }
            var productToRemove = _inputManager.GetItemFromList(products);
            category.ProductsList.Remove(productToRemove);
            unitOfWork.CompleteUnit();
        }
    }
}
