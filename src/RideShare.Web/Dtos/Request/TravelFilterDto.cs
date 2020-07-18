using AutoFilterer.Types;
using RideShare.Web.Entities;
using RideShare.Web.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Dtos.Request
{
    public class TravelFilterDto : PaginationFilterBase
    {
        [ToLowerContainsComparison]
        public string Title { get; set; }

        [ToLowerContainsComparison]
        public string Description { get; set; }

        [ToLowerContainsComparison]
        public string StartingPoint { get; set; }

        [ToLowerContainsComparison]
        public string TargetPoint { get; set; }

        public Range<int> TotalArmchair { get; set; }

        public Range<int> CurrentArmchair { get; set; }

        //public bool IsActive { get; set; } = true;

        public OrderByOption OrderBy { get; set; } = OrderByOption.OrderByDescending;

        public OrderByTravelOptions TravelOptions { get; set; } = OrderByTravelOptions.CurrentArmchair;

        //public Range<DateTime> CreateDate { get; set; }
    }
}
