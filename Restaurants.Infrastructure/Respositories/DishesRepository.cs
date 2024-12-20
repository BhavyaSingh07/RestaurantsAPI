using Restaurants.Domain.Entities;
using Restaurants.Domain.Respositories;
using Restaurants.Infrastructure.Database;

namespace Restaurants.Infrastructure.Respositories
{
    public class DishesRepository(RestaurantDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish dish)
        {
            dbContext.Dishes.Add(dish);
            await dbContext.SaveChangesAsync();
            return dish.Id;
        }

        public async Task Delete(IEnumerable<Dish> dishes)
        {
            dbContext.Dishes.RemoveRange(dishes);
            await dbContext.SaveChangesAsync();
        }
    }
}
