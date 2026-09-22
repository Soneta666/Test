using Ardalis.Specification;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Spesifications
{
    public static class ConferenceHalls
    {
        public class GetAll : Specification<ConferenceHall>
        {
            public GetAll()
            {
                Query
                    .Include(h => h.Services);
            }
        }
        public class ById : Specification<ConferenceHall>
        {
            public ById(uint id)
            {
                Query
                    .Where(h => h.Id == id)
                    .Include(h => h.Services);
            }
        }
    }
}
