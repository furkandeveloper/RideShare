using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Entities
{
    [BsonIgnoreExtraElements]
    public class User : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Surname { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string PhoneNumber { get; set; }
    }
}
