namespace CashFlow.Data.Model
{
    public class Lancamento : BaseEntity
    {
        public Lancamento()
        {
            InseridoEm = DateTime.Now;
            Partition = GetPartitionFromDate(InseridoEm);
            Id = $"{Partition}:{Guid.NewGuid()}";
            
            Excluido = false;
            ExcluidoEm = null;
        }

        public double Valor { get; set; }
        
        public bool Excluido { get; set; }

        public DateTime InseridoEm { get; set; }

        public DateTime? ExcluidoEm { get; set; }

        public static string GetPartitionFromDate(DateTime date)
        {
            return date.ToString("ddMMyyyy");
        }
    }
}