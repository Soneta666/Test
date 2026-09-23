using Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Validators
{
    public class ServiceValidators : AbstractValidator<HallReservationDTO>
    {
        public ServiceValidators()
        {
            RuleFor(h => h.During)
                .Must(x => x > 0)
                .WithMessage("During must be greater than 0.");

            RuleFor(h => h.ConferenceHallId)
                .Must(x => x > 0);

            RuleFor(h => h.Date)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Date cannot be early than today.");
        }
    }
}
