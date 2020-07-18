using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Dtos.Response
{
    public class TravelResponseDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string StartingPoint { get; set; }

        public string TargetPoint { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int TotalArmchair { get; set; }

        public int CurrentArmchair { get; set; }

        public List<UserResponseDto> Users { get; set; } = new List<UserResponseDto>();
    }
}
