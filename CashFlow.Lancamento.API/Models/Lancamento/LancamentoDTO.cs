namespace CashFlow.Lancamento.API.Models.Lancamento
{
    public class LancamentoDTO
    {
        public string Id { get; set; }

        public double Valor { get; set; }

        public bool Excluido { get; set; }

        public DateTime InseridoEm { get; set; }

        public DateTime? ExcluidoEm { get; set; }
    }
}
