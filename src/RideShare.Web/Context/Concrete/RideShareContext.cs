using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using RideShare.Web.Context.Abstractions;
using RideShare.Web.Entities;
using RideShare.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Context.Concrete
{
    public class RideShareContext : DbContext, IRideShareContext
    {
        public RideShareContext(Settings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Database);
        }

        protected readonly IMongoDatabase _database = null;
        /// <summary>
        /// Root of Database object
        /// </summary>
        public new IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }

        public IMongoCollection<User> Users
            => Database.GetCollection<User>(nameof(Users));

        public IMongoCollection<Travel> Travels
            => Database.GetCollection<Travel>(nameof(Travels));

        public IMongoCollection<T> Set<T>(string collection = null)
        {
            return Database.GetCollection<T>(collection ?? typeof(T).Name);
        }
    }
}
