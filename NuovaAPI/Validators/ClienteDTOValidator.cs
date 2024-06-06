using FluentValidation;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Validators
{
    public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
    {
        public ClienteDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull()
                .WithMessage("L'ID del cliente è obbligatorio!");
        }

    }
}
