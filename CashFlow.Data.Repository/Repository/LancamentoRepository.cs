using CashFlow.Data.Model;
using CashFlow.Data.Repository.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CashFlow.Data.Repository.Repository
{
    public class LancamentoRepository : BaseRepository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(ICosmosDbContainerFactory cosmosDbContainerFactory) : base(cosmosDbContainerFactory)
        { }

        public async Task<List<Lancamento>> ListarLancamentos()
        {
            string queryString = @$"SELECT * FROM c";

            FeedIterator<Lancamento> resultSetIterator = _container.GetItemQueryIterator<Lancamento>(new QueryDefinition(queryString));

            var results = new List<Lancamento>();

            while (resultSetIterator.HasMoreResults)
            {
                FeedResponse<Lancamento> response = await resultSetIterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public Lancamento ObterPorId(string id)
        {
            return _container.GetItemLinqQueryable<Lancamento>(true).Where(p=> p.Id == id).AsEnumerable().SingleOrDefault();
        }

        public List<Lancamento> ObterPorDia(DateTime inseridoEm)
        {
            return _container.GetItemLinqQueryable<Lancamento>(true).Where(p => p.Partition == Lancamento.GetPartitionFromDate(inseridoEm)).AsEnumerable().ToList();
        }

        public async Task InserirLancamento(Lancamento lancamento) 
        {
            await _container.CreateItemAsync(lancamento, new PartitionKey(lancamento.Partition));
        }

        public async Task ExcluirLancamento(string id, string partitionKey)
        {
            await _container.DeleteItemAsync<Lancamento>(id, new PartitionKey(partitionKey));
        }
    }
}