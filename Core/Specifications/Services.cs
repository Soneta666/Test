using Ardalis.Specification;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Spesifications
{
    public static class Services
    {
        public class ById : Specification<Service>
        {
            public ById(uint id)
            {
                Query
                    .Where(s => s.Id == id);
            }
        }
    }
}
