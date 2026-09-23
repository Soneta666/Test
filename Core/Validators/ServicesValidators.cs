using Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Validators
{
    public class ServicesValidators : AbstractValidator<ServiceDTO>
    {
        public ServicesValidators()
        {
            RuleFor(h => h.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(h => h.Cost)
                .Must(x => x > 0);
        }
    }
}
