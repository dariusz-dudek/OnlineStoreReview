namespace Codecool.OnlineStore.Data.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetFirstOrDefaultWithProductsList(Func<Order, bool> condition);
        void AddOrderItem(OrderItem orderItem);
        IEnumerable<Order> GetAllOrdersWhere(Func<Order, bool> condition);
    }
}
