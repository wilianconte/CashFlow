using CashFlow.Data.Model;

namespace CashFlow.Domain.Interfaces
{
    public interface ILancamentoService
    {
        Task<Lancamento> ObterPorId(string id);
        Task<List<Lancamento>> ListarLancamentos(int PageNumber, int PageSize);
        Task<List<Lancamento>> ObterPorDia(int PageNumber, int PageSize, DateTime inseridoEm);
        Task<Lancamento> InserirLancamentoAsync(Lancamento lancamento);
        Task<bool> ExcluirLancamento(string id);
        Task<bool> AtualizarLancamento(string id, Lancamento lancamento);

    }
}