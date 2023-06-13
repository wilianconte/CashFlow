using CashFlow.Data.Model;

namespace CashFlow.Domain.Interfaces
{
    public interface ILancamentoService
    {
        Task<List<Lancamento>> ListarLancamentos(int PageNumber, int PageSize);
        Task<Lancamento> ObterPorId(string id);
        Task<List<Lancamento>> ObterPorDia(DateTime inseridoEm);
        Task<bool> InserirLancamentoAsync(Lancamento lancamento);
        Task<bool> ExcluirLancamento(string id);
        Task<bool> AtualizarLancamento(string id, Lancamento lancamento);

    }
}