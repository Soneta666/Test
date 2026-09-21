using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    // DTO HallReservation
    public class HallReservationDTO
    {
        public uint Id { get; set; }
        public DateTime Date { get; set; }
        public uint During { get; set; }

        public uint ConferenceHallId { get; set; }
        public ConferenceHallDTO? ConferenceHall { get; set; }
        public ICollection<ServiceDTO> Services { get; set; }
    }
}
