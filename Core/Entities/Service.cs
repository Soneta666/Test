using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    // Entity Service with ptoperties: Name, Cost
    public class Service
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint Cost { get; set; }
    }
}
