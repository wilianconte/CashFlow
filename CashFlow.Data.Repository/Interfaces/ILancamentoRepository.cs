using CashFlow.Data.Model;

namespace CashFlow.Data.Repository.Interfaces
{
    public interface ILancamentoRepository
    {
        Task<List<Lancamento>> ListarLancamentos();

        Lancamento ObterPorId(string id);

        List<Lancamento> ObterPorDia(DateTime inseridoEm);

        Task InserirLancamento(Lancamento lancamento);

        Task ExcluirLancamento(string id, string partitionKey);

        Task AtualizarLancamento(Lancamento dbLancamento);
    }
}