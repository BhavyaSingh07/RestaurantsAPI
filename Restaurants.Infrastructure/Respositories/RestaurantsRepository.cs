using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
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

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbcontext.Restaurants
                .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower) || r.Description.ToLower().Contains(searchPhraseLower)));
                
            var totalCount = await baseQuery.CountAsync();

            if(sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name), r=>r.Name },
                    {nameof(Restaurant.Category), r=>r.Category }
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber-1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
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
