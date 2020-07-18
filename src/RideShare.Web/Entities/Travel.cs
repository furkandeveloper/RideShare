using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Entities
{
    [BsonIgnoreExtraElements]
    public class Travel : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string StartingPoint { get; set; }

        public string TargetPoint { get; set; }

        public int TotalArmchair { get; set; }

        public int CurrentArmchair { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
