using CashFlow.Data.Model;
using CashFlow.Data.Repository.Interfaces;
using CashFlow.Domain.Interfaces;

namespace CashFlow.Domain.Services
{
    public class LancamentoService : ILancamentoService
    {
        private readonly ILancamentoRepository LancamentoRepository;

        public LancamentoService(ILancamentoRepository lancamentoRepository)
        {
            LancamentoRepository = lancamentoRepository;
        }

        public async Task<List<Lancamento>> ListarLancamentos(int PageNumber, int PageSize)
        {
            return await LancamentoRepository.ListarLancamentos(PageNumber, PageSize);
        }

        public async Task<Lancamento> ObterPorId(string id)
        {
            return await LancamentoRepository.ObterPorId(id);
        }

        public async Task<List<Lancamento>> ObterPorDia(DateTime inseridoEm)
        {
            return await LancamentoRepository.ObterPorDia(inseridoEm);
        }

        public async Task<bool> InserirLancamentoAsync(Lancamento lancamento)
        {
            try
            {
                lancamento.Partition = lancamento.InseridoEm.ToString("ddMMyyyy");
                lancamento.Id = $"{lancamento.Partition}:{Guid.NewGuid()}";
                lancamento.InseridoEm = DateTime.Now;

                await LancamentoRepository.InserirLancamento(lancamento);

                return true;
            }
            catch
            {
                return false;
            }
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
