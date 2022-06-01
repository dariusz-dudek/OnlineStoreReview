namespace Codecool.OnlineStore.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public Credentials Credentials { get; set; }
    }
}
