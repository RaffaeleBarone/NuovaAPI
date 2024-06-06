using FluentValidation;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull()
                .WithMessage("L'ID del cliente è obbligatorio!");
        }
       
    }
}
