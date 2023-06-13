using CashFlow.Domain.Interfaces;
using CashFlow.Lancamento.API.Models.Lancamento;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Lancamento.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LancamentoController : ControllerBase
    {
        private readonly ILogger<LancamentoController> _logger;
        private readonly ILancamentoService _lancamentoService;

        public LancamentoController
        (
            ILogger<LancamentoController> logger,
            ILancamentoService lancamentoService
        )
        {
            _logger = logger;
            _lancamentoService = lancamentoService;
        }

        [HttpGet]
        public async Task<List<Data.Model.Lancamento>> ListarLancamentos()
        {
            return await _lancamentoService.ListarLancamentos();
        }

        [HttpGet("{id}")]
        public async Task<Data.Model.Lancamento> ObterPorId(string id)
        {
            return _lancamentoService.ObterPorId(id);
        }
        
        [HttpGet("ObterPorDia/{dia}")]
        public async Task<List<Data.Model.Lancamento>> ObterPorDia(DateTime dia)
        {
            return _lancamentoService.ObterPorDia(dia);
        }

        [HttpPost]
        public async Task<bool> InserirLancamento([FromBody] LancamentoInsertDTO lancamento)
        {
            return await _lancamentoService.InserirLancamentoAsync(new Data.Model.Lancamento
            {
                Valor = lancamento.Valor
            });
        }

        [HttpPut("{id}")]
        public async Task<bool> Put(string id, [FromBody] LancamentoUpdateDTO lancamento)
        {
            return await _lancamentoService.AtualizarLancamento(id, new Data.Model.Lancamento 
            {
                Valor = lancamento.Valor
            });
        }

        [HttpDelete]
        public async Task<bool> ExcluirLancamento(string id, DateTime InseridoEm)
        {
            return await _lancamentoService.ExcluirLancamento(id);
        }
    }
}