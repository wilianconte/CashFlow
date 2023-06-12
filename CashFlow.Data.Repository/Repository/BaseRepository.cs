using CashFlow.Data.Model;
using CashFlow.Data.Repository.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CashFlow.Data.Repository.Repository
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        private string containerId = typeof(T).Name;
        
        public Container _container;
        
        public BaseRepository(ICosmosDbContainerFactory cosmosDbContainerFactory)
        {
            _container = cosmosDbContainerFactory.GetContainer(containerId);
        }
    }
}
