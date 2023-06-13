using CashFlow.Data.Model;
using CashFlow.Data.Repository.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CashFlow.Data.Repository.Repository
{
    public class LancamentoRepository : BaseRepository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(ICosmosDbContainerFactory cosmosDbContainerFactory) : base(cosmosDbContainerFactory)
        { }

        public async Task<Lancamento> ObterPorId(string id)
        {
            var partitionKey = id.Split(':')[0];

            return await _container.ReadItemAsync<Lancamento>(id: id, partitionKey: new PartitionKey(partitionKey));
        }

        public async Task<List<Lancamento>> ListarLancamentos(int PageNumber, int PageSize)
        {
            string queryString = @$"SELECT * FROM c OFFSET {(PageNumber - 1) * PageSize}  LIMIT {PageSize}";

            var query = new QueryDefinition(queryString);

            var iterator = _container.GetItemQueryIterator<Lancamento>(query);

            var results = new List<Lancamento>();

            while (iterator.HasMoreResults)
            {
                FeedResponse<Lancamento> response = await iterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<List<Lancamento>> ObterPorDia(int PageNumber, int PageSize, DateTime inseridoEm)
        {
            var results = new List<Lancamento>();

            var queryString = $"SELECT * FROM Lancamento p WHERE p.Partition = @partition OFFSET {(PageNumber - 1) * PageSize}  LIMIT {PageSize}";

            var query = new QueryDefinition( query: queryString)
                .WithParameter("@partition", Lancamento.GetPartitionFromDate(inseridoEm));

            var iterator = _container.GetItemQueryIterator<Lancamento>(
                queryDefinition: query
            );

            while (iterator.HasMoreResults)
            {
                FeedResponse<Lancamento> response = await iterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task InserirLancamento(Lancamento lancamento) 
        {
            await _container.CreateItemAsync(lancamento, new PartitionKey(lancamento.Partition));
        }

        public async Task ExcluirLancamento(string id, string partitionKey)
        {
            await _container.DeleteItemAsync<Lancamento>(id, new PartitionKey(partitionKey));
        }

        public async Task AtualizarLancamento(Lancamento dbLancamento)
        {
            await _container.UpsertItemAsync(dbLancamento);
        }
    }
}
