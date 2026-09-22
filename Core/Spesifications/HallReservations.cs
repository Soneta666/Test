using Ardalis.Specification;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Spesifications
{
    public static class HallReservations
    {
        public class GetAll : Specification<HallReservation>
        {
            public GetAll()
            {
                Query
                    .Include(h => h.ConferenceHall)
                    .Include(h => h.Services);
            }
        }
        public class ById : Specification<HallReservation>
        {
            public ById(uint id)
            {
                Query
                    .Where(h => h.Id == id)
                    .Include(h => h.ConferenceHall)
                    .Include(h => h.Services);
            }
        }
    }
}
