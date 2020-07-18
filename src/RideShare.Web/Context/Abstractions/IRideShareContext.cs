using MongoDB.Driver;
using RideShare.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Context.Abstractions
{
    public interface IRideShareContext
    {
        IMongoCollection<Travel> Travels { get; }
        IMongoCollection<User> Users { get; }

        IMongoCollection<T> Set<T>(string collection = null);

        IMongoDatabase Database { get; }
    }
}
