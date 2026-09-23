using Ardalis.Specification;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class AvailableHalls : Specification<HallReservation>
    {
        public AvailableHalls(
            DateTime date,
            uint during)
        {
            var endDate = date.AddHours(during);

            Query
                .Where(reservation =>
                        reservation.Date < endDate &&
                        reservation.Date.AddHours(reservation.During) > date
                    );
        }
    }
}
