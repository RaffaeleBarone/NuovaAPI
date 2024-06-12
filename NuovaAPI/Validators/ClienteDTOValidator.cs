using FluentValidation;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Validators
{
    public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
    {
        public ClienteDTOValidator()
        {
            //RuleFor(x => x.Nome).NotEmpty().NotNull()
            //    .WithMessage("Il nome del cliente è obbligatorio!");
        }

    }
}
