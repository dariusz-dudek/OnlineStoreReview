namespace Codecool.OnlineStore.Data.Entities
{
    public class Customer : User
    {
        public ShoppingCart ShoppingCart { get; set; } 
        public List<Rating> Ratings { get; set; }

        public Customer()
        {
            Ratings = new List<Rating>();
        }
    }
}
