using AutoFilterer.Extensions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using RideShare.Web.Context.Abstractions;
using RideShare.Web.Dtos.Request;
using RideShare.Web.Entities;
using RideShare.Web.Helpers.Extensions;
using RideShare.Web.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml;

namespace RideShare.Web.Repositories.Concrete
{
    public class TravelRepository : BaseRepository<Travel>, ITravelRepository
    {
        public TravelRepository(IRideShareContext context) : base(context)
        {
            base.collection = () => nameof(context.Travels);
        }

        public async Task<List<Travel>> FilterTravelAsync([NotNull] TravelFilterDto filter)
        {
            return context.Travels.AsQueryable().ApplyOrder(filter.TravelOptions,filter.OrderBy).ApplyFilter(filter).ToList();
        }

        public async Task DeleteUserOfTravelAsync([NotNull] string userId)
        {
            var list =  context.Travels.AsQueryable().Where(x => x.Users.Any(a => a.Id == userId)).ToList();

            foreach (var item in list)
            {
                item.Users.RemoveAll(x => x.Id == userId);
                item.CurrentArmchair = item.CurrentArmchair - 1;
                await base.ReplaceAsync(item);
            }

        }
    }
}
