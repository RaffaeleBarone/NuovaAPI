using FluentValidation;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Validators
{
    public class VetrinaValidator : AbstractValidator<Vetrina>
    {
        public VetrinaValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull()
                .WithMessage("L'ID della vetrina è obbligatorio!");
        }
    }
}
