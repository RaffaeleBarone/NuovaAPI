using FluentValidation;
using NuovaAPI.Commons.DTO;
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
            services.AddScoped<IValidator<ProdottoDTO>, ProdottoDTOValidator>();

            return services;
        }
    }
}
