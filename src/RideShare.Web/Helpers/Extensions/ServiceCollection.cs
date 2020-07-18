using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using RideShare.Web.Context.Abstractions;
using RideShare.Web.Context.Concrete;
using RideShare.Web.Repositories.Abstractions;
using RideShare.Web.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Helpers.Extensions
{
    /// <summary>
    /// Service collection extension method
    /// </summary>
    public static class ServiceCollection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRideShareContext, RideShareContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
