namespace Codecool.OnlineStore.Data.Context
{
    public class StoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ProductShoppingCart> ProductShoppingCarts { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=OnlineStore;Integrated Security=True");   
        //TODO: Change to connection string from app.config
        //var connectionString = ConfigurationManager.ConnectionStrings["OnlineStoreDB"].ConnectionString;
        //    optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShoppingCart>(eb =>
            {
                eb.HasMany(c => c.ProductList)
                .WithMany(p => p.ShoppingCart)
                .UsingEntity<ProductShoppingCart>(
                    p => p.HasOne(c => c.Product)
                    .WithMany()
                    .HasForeignKey(c => c.ProductId),

                    c => c.HasOne(p => p.ShoppingCart)
                    .WithMany()
                    .HasForeignKey(p => p.ShoppingCartId),

                    c => 
                    {
                        c.HasKey(x => new { x.ProductId, x.ShoppingCartId });
                        c.Property(x => x.ProductQuantity).HasDefaultValue(1);
                    });
            });
        }
    }
}
