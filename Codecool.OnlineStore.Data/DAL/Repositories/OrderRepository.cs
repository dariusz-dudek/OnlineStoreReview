namespace Codecool.OnlineStore.Data.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(StoreContext context)
            : base(context) { }

        public void AddOrderItem(OrderItem orderItem)
            => StoreContext.OrderItems.Add(orderItem);

        public Order GetFirstOrDefaultWithProductsList(Func<Order, bool> condition)
        {
            return StoreContext.Orders
                .Include(x => x.ItemsList)
                .Where(condition)
                .FirstOrDefault();
        }

        public IEnumerable<Order> GetAllOrdersWhere(Func<Order, bool> condition)
        {
            return StoreContext.Orders
               .Include(x => x.ItemsList)
               .Include(x => x.AssignedUser)
               .Where(condition);
               
        }
    }
}
