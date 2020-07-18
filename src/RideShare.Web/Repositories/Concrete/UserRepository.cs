using RideShare.Web.Context.Abstractions;
using RideShare.Web.Entities;
using RideShare.Web.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Repositories.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IRideShareContext context) : base(context)
        {
            base.collection = () => nameof(context.Users);
        }
    }
}
