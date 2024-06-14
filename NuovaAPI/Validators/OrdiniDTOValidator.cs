using FluentValidation;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Validators
{
    public class OrdiniDTOValidator : AbstractValidator<OrdiniDTO>
    {
        public OrdiniDTOValidator()
        {
            RuleFor(x => x.CodiceOrdine).NotNull()
                .WithMessage("Il codice dell'ordine è obbligatorio");
        }
    }
}

