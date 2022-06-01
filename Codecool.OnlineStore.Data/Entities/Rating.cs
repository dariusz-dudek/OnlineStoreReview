namespace Codecool.OnlineStore.Data.Entities
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int ProductId { get; set; }
        public int NumberOfRatings { get; set; }
        public decimal CurrentRating { get; set; }
        public List<Customer> Customers { get; set; }

        public Rating()
        {
            Customers = new List<Customer>();
        }
    }
}
