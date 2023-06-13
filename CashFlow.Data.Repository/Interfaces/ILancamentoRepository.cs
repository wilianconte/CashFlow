using CashFlow.Data.Model;

namespace CashFlow.Data.Repository.Interfaces
{
    public interface ILancamentoRepository
    {
        Task<Lancamento> ObterPorId(string id);

        Task<List<Lancamento>> ListarLancamentos(int PageNumber, int PageSize);

        Task<List<Lancamento>> ObterPorDia(int PageNumber, int PageSize, DateTime inseridoEm);

        Task InserirLancamento(Lancamento lancamento);

        Task ExcluirLancamento(string id, string partitionKey);

        Task AtualizarLancamento(Lancamento dbLancamento);
    }
}