namespace CashFlow.Data.Repository.Infrastructure
{
    public class ContainerInfo
    {
        public string? NameDb { get; set; }
        public string? Name { get; set; }
        public string? PartitionKey { get; set; }
        public int MaxRU { get; set; }
    }
}
