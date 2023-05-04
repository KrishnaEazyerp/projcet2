using System;
using System.Collections.Generic;

namespace assignment2.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public int BuildingId { get; set; }
        public string Address { get; set; } = null!;
        public int ZipCode { get; set; }
        public string ManagerName { get; set; } = null!;
    }
}
