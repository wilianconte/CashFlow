using CashFlow.Data.Model;
using CashFlow.Data.Repository.Interfaces;
using CashFlow.Domain.Interfaces;

namespace CashFlow.Domain.Services
{
    public class LancamentoService : ILancamentoService
    {
        private readonly ILancamentoRepository LancamentoRepository;

        public async Task<Lancamento> ObterPorId(string id)
        {
            return await LancamentoRepository.ObterPorId(id);
        }

        public LancamentoService(ILancamentoRepository lancamentoRepository)
        {
            LancamentoRepository = lancamentoRepository;
        }

        public async Task<List<Lancamento>> ListarLancamentos(int PageNumber, int PageSize)
        {
            return await LancamentoRepository.ListarLancamentos(PageNumber, PageSize);
        }

        public async Task<List<Lancamento>> ObterPorDia(int PageNumber, int PageSize, DateTime inseridoEm)
        {
            return await LancamentoRepository.ObterPorDia(PageNumber, PageSize, inseridoEm);
        }

        public async Task<Lancamento> InserirLancamentoAsync(Lancamento lancamento)
        {
            lancamento.Partition = lancamento.InseridoEm.ToString("ddMMyyyy");
            lancamento.Id = $"{lancamento.Partition}:{Guid.NewGuid()}";
            lancamento.InseridoEm = DateTime.Now;

            return await LancamentoRepository.InserirLancamento(lancamento);
        }

        public async Task<bool> ExcluirLancamento(string id)
        {
            try
            {
                await LancamentoRepository.ExcluirLancamento(id, id.Split(":")[0]);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarLancamento(string id, Lancamento lancamento)
        {
            var dbLancamento = await ObterPorId(id);

            dbLancamento.Valor = lancamento.Valor;

            await LancamentoRepository.AtualizarLancamento(dbLancamento);

            return true;
        }
    }
}
