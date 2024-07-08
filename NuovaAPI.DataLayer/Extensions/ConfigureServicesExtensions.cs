using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Infrastructure;
using NuovaAPI.DataLayer.Infrastructure.Implementations;
using NuovaAPI.DataLayer.Manager;

namespace NuovaAPI.DataLayer.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepository<Cliente>, Repository<Cliente>>();
            services.AddScoped<IRepository<OrdineProdotto>, Repository<OrdineProdotto>>();
            services.AddScoped<IRepository<Ordini>, Repository<Ordini>>();
            services.AddScoped<IRepository<Prodotto>, Repository<Prodotto>>();
            services.AddScoped<IRepository<Vetrina>, Repository<Vetrina>>();
            services.AddScoped<IRepository<Taxonomy>, Repository<Taxonomy>>();
            services.AddScoped<IRepository<Termini>, Repository<Termini>>();

            services.AddScoped<IVetrinaManager, VetrinaManager>();
            services.AddScoped<IProdottoManager, ProdottoManager>();
            services.AddScoped<IClienteManager, ClienteManager>();
            services.AddScoped<IOrdiniManager, OrdiniManager>();
            services.AddScoped<IOrdineProdottoManager, OrdineProdottoManager>();
            services.AddScoped<ITaxonomyManager, TaxonomyManager>();

            return services;
        }
    }
}