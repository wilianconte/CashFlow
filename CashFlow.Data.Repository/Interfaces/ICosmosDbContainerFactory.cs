using Microsoft.Azure.Cosmos;

namespace CashFlow.Data.Repository.Interfaces
{
    public interface ICosmosDbContainerFactory
    {
        Container GetContainer(string containerId);
    }
}
