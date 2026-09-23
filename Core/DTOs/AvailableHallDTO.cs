using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class AvailableHallDTO
    {
        public DateTime Date { get; set; }
        public uint During { get; set; }
        public uint Capacity { get; set; }
    }
}
