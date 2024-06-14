using FluentValidation;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Validators
{
    public class OrdiniValidator : AbstractValidator<Ordini>
    {
        public OrdiniValidator()
        {
            RuleFor(x => x.CodiceOrdine).NotNull()
                .WithMessage("Il codice dell'ordine è obbligatorio");
        }
    }
}
