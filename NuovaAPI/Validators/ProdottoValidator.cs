using FluentValidation;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Commons.Validators
{
    public class ProdottoValidator : AbstractValidator<Prodotto>
    {
        public ProdottoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull()
                .WithMessage("L'ID del prodotto è obbligatorio!");
            RuleFor(x => x.NomeProdotto).NotNull().NotEmpty()
                .WithMessage("Il nome del prodotto è obbligatorio!").Length(1, 100)
                .WithMessage("Il nome del prodotto deve avere lunghezza compresa tra 1 e 100 caratteri!");
            RuleFor(x => x.Prezzo).GreaterThanOrEqualTo(0)
                .WithMessage("Il prezzo del prodotto non può essere un valore negativo");
        }
    }
}
