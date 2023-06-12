namespace CashFlow.Data.Repository.Infrastructure
{
    public class CosmosDbSettings
    {
        public List<string>? DataBaseName { get; set; }
        public List<ContainerInfo>? Containers { get; set; }
        public int DataBaseThroughput { get; set; }

        public CosmosDbSettings()
        {
            DataBaseName = new List<string>();
            Containers = new List<ContainerInfo>();
        }
    }
}
