using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Manager;
using System.Configuration;

namespace NuovaAPI.DataLayer.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IVetrinaManager, VetrinaManager>();
            services.AddScoped<IProdottoManager, ProdottoManager>();
            services.AddScoped<IClienteManager, ClienteManager>();
            services.AddScoped<IOrdiniManager, OrdiniManager>();
            return services;
        }
    }
}