using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    //DTO ConferenceHall
    public class ConferenceHallDTO
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint Capasity { get; set; }
        public uint BaseRentalCost { get; set; }

        public ICollection<ServiceDTO>? Services { get; set; }
    }
}
