using CashFlow.Data.Repository.Infrastructure;
using CashFlow.Data.Repository.Interfaces;
using CashFlow.Data.Repository.Repository;
using CashFlow.Domain.Interfaces;
using CashFlow.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Crosscutting.IoC
{
    public static class IoC
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            CosmosDbAccess cosmosDbAccess = configuration.GetSection("CosmoDbAccess").Get<CosmosDbAccess>();
            CosmosDbSettings settings = configuration.GetSection("CosmosDb").Get<CosmosDbSettings>();

            //CosmosDb
            services.AddSingleton<ICosmosDbContainerFactory>(new CosmosDbContainerFactory(cosmosDbAccess));

            //Repositorios
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();

            //Serviços
            services.AddScoped<ILancamentoService, LancamentoService>();
        }
    }
}