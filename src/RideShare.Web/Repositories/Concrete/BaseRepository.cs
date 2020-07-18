using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using RideShare.Web.Context.Abstractions;
using RideShare.Web.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RideShare.Web.Repositories.Concrete
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        private protected readonly IRideShareContext context;
        private protected Func<string> collection = () => null;
        private protected Func<IRideShareContext, IMongoCollection<TEntity>> set;
        private string _primaryKey;

        public string PrimaryKey
        {
            get
            {
                if (!string.IsNullOrEmpty(_primaryKey))
                    return _primaryKey;
                var property = typeof(TEntity).GetProperties().FirstOrDefault(x => x.GetCustomAttributes(typeof(BsonIdAttribute), false).Count() > 0);
                var attribute = property.GetCustomAttributes(typeof(BsonElementAttribute), false).FirstOrDefault();
                _primaryKey = (attribute as BsonElementAttribute)?.ElementName ?? property.Name;
                return _primaryKey;
            }
        }
        public BaseRepository(IRideShareContext context)
        {
            this.context = context;
            set = (ctx) => ctx.Set<TEntity>(collection()); // default set method
        }

        public virtual TEntity Add(TEntity value)
        {
            set(context).InsertOne(value);
            return value;
        }
        public virtual async Task<TEntity> AddAsync(TEntity value)
        {
            await set(context).InsertOneAsync(value);
            return value;
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> func)
        {
            return set(context).AsQueryable().FirstOrDefault(func);
        }
        public virtual Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> query = null)
        {
            TaskCompletionSource<TEntity> tcs = new TaskCompletionSource<TEntity>();
            var entity = query == null ? set(context).AsQueryable().FirstOrDefault() : set(context).AsQueryable().FirstOrDefault(query);
            tcs.TrySetResult(entity);
            return tcs.Task;
        }

        public virtual TEntity GetSingle(string id)
        {
            return set(context).Find(new BsonDocument(new BsonElement("_id", BsonValue.Create(id)))).FirstOrDefault();
        }

        public virtual Task<TEntity> GetSingleAsync(string id)
        {
            return Task.Run(() => GetSingle(id));
        }
        public virtual Task<List<TEntity>> GetMultiple(Expression<Func<TEntity, bool>> func)
        {
            return Task.FromResult(Get().Where(func).ToList());
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return Get().Where(filter);
        }

        public virtual IQueryable<TEntity> Get()
        {
            return set(context).AsQueryable();
        }

        public virtual void Update(TEntity value)
        {
            var filter = new BsonDocument(new BsonElement("_id", BsonValue.Create(value.ToBsonDocument()["_id"])));
            var edited = set(context).Find(filter).First();
            foreach (var property in typeof(TEntity).GetProperties())
            {
                var _val = property.GetValue(value);
                if (!_val.Equals(default))
                    property.SetValue(edited, _val);
            }
            set(context).ReplaceOne(filter, edited);
        }
        public virtual async Task UpdateAsync(TEntity value)
        {
            var filter = new BsonDocument(new BsonElement("_id", BsonValue.Create(value.ToBsonDocument()["_id"])));
            var edited = await set(context).Find(filter).FirstAsync();
            foreach (var property in typeof(TEntity).GetProperties())
            {
                var _val = property.GetValue(value);
                if (!_val.Equals(default))
                    property.SetValue(edited, _val);
            }
            await set(context).ReplaceOneAsync(filter, edited);
        }

        public virtual void Replace(TEntity value)
        {
            set(context).ReplaceOne(new BsonDocument(new BsonElement("_id", BsonValue.Create(value.ToBsonDocument()["_id"]))), value);
        }

        public virtual Task ReplaceAsync(TEntity value)
        {
            return set(context).ReplaceOneAsync(new BsonDocument(new BsonElement("_id", BsonValue.Create(value.ToBsonDocument()["_id"]))), value);
        }

        public virtual void Delete(TEntity value)
        {
            set(context).DeleteOne(new BsonDocument(new BsonElement(PrimaryKey, BsonValue.Create(value.ToBsonDocument()["_id"]))));
        }
        public virtual void Delete(string id)
        {
            set(context).DeleteOne(new BsonDocument(new BsonElement(PrimaryKey, BsonValue.Create(id))));
        }
        public virtual Task DeleteAsync(TEntity value)
        {
            return set(context).DeleteOneAsync(new BsonDocument(new BsonElement("_id", BsonValue.Create(value.ToBsonDocument()["_id"]))));
        }
        public virtual Task DeleteAsync(string id)
        {
            return set(context).DeleteOneAsync(new BsonDocument(new BsonElement("_id", BsonValue.Create(id))));
        }
    }
}
