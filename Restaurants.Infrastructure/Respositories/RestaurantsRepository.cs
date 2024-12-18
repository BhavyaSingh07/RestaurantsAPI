using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Respositories;
using Restaurants.Infrastructure.Database;

namespace Restaurants.Infrastructure.Respositories
{
    internal class RestaurantsRepository(RestaurantDbContext dbcontext) : IRestaurantRepository
    {
        public async Task<int> CreateAsync(Restaurant restaurant)
        {
            dbcontext.Restaurants.Add(restaurant);
            await dbcontext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            dbcontext.Remove(restaurant);
            await dbcontext.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbcontext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await dbcontext.Restaurants
                .Include(t => t.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id);

            return restaurant;
        }

        public Task UpdateChangesAsync() => dbcontext.SaveChangesAsync();
    }
}
