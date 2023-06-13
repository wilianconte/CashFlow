using CashFlow.Domain.Interfaces;
using CashFlow.Lancamento.API.Filter;
using CashFlow.Lancamento.API.Models.Lancamento;
using CashFlow.Lancamento.API.Wrappers;
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
        public async Task<IActionResult> ListarLancamentos([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var model = await _lancamentoService.ListarLancamentos(validFilter.PageNumber, validFilter.PageSize);

            var dto = model.Select(p => new LancamentoDTO 
            {
                Id = p.Id,
                Valor = p.Valor,
                InseridoEm = p.InseridoEm,
                Excluido = p.Excluido,
                ExcluidoEm = p.ExcluidoEm
            }).ToList();

            return Ok(new Response<List<LancamentoDTO>>(dto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(string id)
        {
            var model = await _lancamentoService.ObterPorId(id);

            var dto = new LancamentoDTO 
            {
                Id = model.Id,
                Valor = model.Valor,
                InseridoEm = model.InseridoEm,
                Excluido = model.Excluido,
                ExcluidoEm = model.ExcluidoEm
            };

            return Ok(new Response<LancamentoDTO>(dto));
        }
        
        [HttpGet("ObterPorDia/{dia}")]
        public async Task<IActionResult> ObterPorDia([FromQuery] PaginationFilter filter, DateTime dia)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var model = await _lancamentoService.ObterPorDia(validFilter.PageNumber, validFilter.PageSize, dia);

            var dto = model.Select(p => new LancamentoDTO
            {
                Id = p.Id,
                Valor = p.Valor,
                InseridoEm = p.InseridoEm,
                Excluido = p.Excluido,
                ExcluidoEm = p.ExcluidoEm
            }).ToList();

            return Ok(new Response<List<LancamentoDTO>>(dto));

        }

        [HttpPost]
        public async Task<IActionResult> InserirLancamento([FromBody] LancamentoInsertDTO lancamento)
        {
            var model =  await _lancamentoService.InserirLancamentoAsync(new Data.Model.Lancamento
            {
                Valor = lancamento.Valor
            });

            var dto = new LancamentoDTO
            {
                Id = model.Id,
                Valor = model.Valor,
                InseridoEm = model.InseridoEm,
                Excluido = model.Excluido,
                ExcluidoEm = model.ExcluidoEm
            };

            return CreatedAtAction(nameof(ObterPorId), new { Id = dto.Id }, new Response<LancamentoDTO>(dto));
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
        public async Task<bool> ExcluirLancamento(string id)
        {
            return await _lancamentoService.ExcluirLancamento(id);
        }

        #region Referencias
        //https://codewithmukesh.com/blog/pagination-in-aspnet-core-webapi/
        #endregion
    }
}