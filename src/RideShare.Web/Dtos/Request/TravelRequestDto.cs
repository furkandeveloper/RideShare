using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Dtos.Request
{
    public class TravelRequestDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string StartingPoint { get; set; }

        public string TargetPoint { get; set; }

        public int TotalArmchair { get; set; }
    }
}
