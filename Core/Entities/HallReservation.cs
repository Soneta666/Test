using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    // Entity HallReservation with properties: Date, During
    public class HallReservation
    {
        public uint Id { get; set; }
        public DateTime Date { get; set; }
        public uint During { get; set; }


    }
}
