using FluentValidation;
using NuovaAPI.Commons.DTO;
using NuovaAPI.Commons.Validators;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.Validators;
using NuovaAPI.Worker_Services;

namespace NuovaAPI.Extensions
{
    public static class ConfigureAPIServicesExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IProdottoWorkerService, ProdottoWorkerService>();
            services.AddScoped<IVetrinaWorkerService, VetrinaWorkerService>();
            services.AddScoped<IClienteWorkerService, ClienteWorkerService>();
            services.AddScoped<IOrdiniWorkerService, OrdiniWorkerService>();
            services.AddScoped<IOrdineProdottoWorkerService,  OrdineProdottoWorkerService>();
            services.AddScoped<ITaxonomyWorkerService, TaxonomyWorkerService>();

            services.AddScoped<IValidator<ProdottoDTO>, ProdottoDTOValidator>();
            services.AddScoped<IValidator<VetrinaDTO>,  VetrinaDTOValidator>();
            services.AddScoped<IValidator<ClienteDTO>, ClienteDTOValidator>();
            services.AddScoped<IValidator<Cliente>,  ClienteValidator>();
            services.AddScoped<IValidator<Vetrina>, VetrinaValidator>();
            services.AddScoped<IValidator<Prodotto>, ProdottoValidator>();

            return services;
        }
    }
}
