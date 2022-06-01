namespace Codecool.OnlineStore.Data.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsFeatured { get; set; }
        public ICollection<Product> ProductsList { get; set; }

        public Category()
        {
            ProductsList = new List<Product>();
        }

        public override string ToString()
            => $"{CategoryName}"; 
    }
}
