namespace Codecool.OnlineStore.Controller
{
    public interface IProductHandler
    {
        void ChangeActiveState(Product product, bool state);
        void CreateNewProduct();
        void EditAmount(Product product);
        void EditDescription(Product product);
        void EditName(Product product);
        void EditPrice(Product product);
        void EditProduct();
        List<Product> GetAllProductsFromSpecifiedCategory();
        void RunFeatureBasedOn(int userInput);
    }
}