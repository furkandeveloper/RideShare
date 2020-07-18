using RideShare.Web.Dtos.Request;
using RideShare.Web.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Repositories.Abstractions
{
    public interface ITravelRepository : IBaseRepository<Travel>
    {
        Task<List<Travel>> FilterTravelAsync([NotNull]TravelFilterDto filter);

        Task DeleteUserOfTravelAsync([NotNull]string userId);
    }
}
