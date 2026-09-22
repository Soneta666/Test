using Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Validators
{
    public class ConferenceHallValidators : AbstractValidator<ConferenceHallDTO>
    {
        public ConferenceHallValidators()
        {
            RuleFor(h => h.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(h => h.BaseRentalCost)
                .Must(x => x > 0)
                .NotEmpty();

            RuleFor(h => h.Capasity)
                .Must(x => x > 0)
                .NotEmpty();
        }
    }
}
