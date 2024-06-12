using FluentValidation;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Validators
{
    public class VetrinaDTOValidator : AbstractValidator<VetrinaDTO>
    {
        public VetrinaDTOValidator()
        {
            RuleFor(x => x.CodiceVetrina).NotEmpty().NotNull()
                .WithMessage("Il codice della vetrina è obbligatorio!");
        }
    }
}
