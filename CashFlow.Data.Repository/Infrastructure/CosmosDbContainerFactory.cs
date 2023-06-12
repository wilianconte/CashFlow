using CashFlow.Data.Repository.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CashFlow.Data.Repository.Infrastructure
{
    public class CosmosDbContainerFactory : ICosmosDbContainerFactory
    {
        private readonly string _dataBaseId;
        private readonly CosmosClient _cosmosClient;

        public CosmosDbContainerFactory(CosmosDbAccess cosmosDbAccess)
        {
            _cosmosClient = new CosmosClient(cosmosDbAccess.EndPoint, cosmosDbAccess.TokenCredential, new CosmosClientOptions { ApplicationName = cosmosDbAccess.ApplicationName });
            _dataBaseId = cosmosDbAccess.DataBaseId;
        }

        public Container GetContainer(string containerId)
        {
            return _cosmosClient.GetContainer(_dataBaseId, containerId);
        }
    }
}