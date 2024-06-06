using FluentValidation;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Validators
{
    public class VetrinaDTOValidator : AbstractValidator<VetrinaDTO>
    {
        public VetrinaDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull()
                .WithMessage("L'ID della vetrina è obbligatorio!");
        }
    }
}
