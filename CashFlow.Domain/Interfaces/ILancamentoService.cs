using CashFlow.Data.Model;

namespace CashFlow.Domain.Interfaces
{
    public interface ILancamentoService
    {
        Task<List<Lancamento>> ListarLancamentos();
        Lancamento ObterPorId(string id);
        List<Lancamento> ObterPorDia(DateTime inseridoEm);
        Task<bool> InserirLancamentoAsync(Lancamento lancamento);
        Task<bool> ExcluirLancamento(string id);
    }
}