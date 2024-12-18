using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Respositories;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //services.AddScoped<IRestaurantsService, RestaurantsService>();
            var applicationAssembly = typeof(ServiceCollection).Assembly;
            //services.AddMediatR(c => c.RegisterServicesFromAssembly(applicationAssembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllRestaurantsQueryHandler>());


            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
