namespace Codecool.OnlineStore.Factories
{
    public class UnitOfWorkFactory : IFactory<UnitOfWork>
    {
        private StoreContext _storeContext;

        public UnitOfWorkFactory(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public UnitOfWork GetNew()
            => new UnitOfWork(_storeContext);
    }
}
