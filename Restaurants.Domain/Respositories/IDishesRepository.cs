using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Respositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish dish);
        Task Delete(IEnumerable<Dish> dishes);
    }
}
