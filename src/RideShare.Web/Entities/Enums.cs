using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderByOption
    {
        OrderBy,
        OrderByDescending
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderByTravelOptions
    {
        TotalArmchair,
        CurrentArmchair,
        CreateDate,
        UpdateDate,
        Title,
        Description,
        StartingPoint,
        TargetPoint
    }
}
