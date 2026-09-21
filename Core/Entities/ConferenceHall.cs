using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    //Entity ConferenceHall with properties: Name, Capasity, BaseRentalCost
    public class ConferenceHall
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint Capasity { get; set; }
        public uint BaseRentalCost { get; set; }

        public ICollection<Service> Servises { get; set; }
    }
}
