using Xunit;
using Restaurants.Application.Restaurants.DTOs;
using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs.Tests
{
    public class RestaurantsProfileTests
    {
        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
        {
            //arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });

            var mapper = configuration.CreateMapper();

            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Test",
                Description = "Test des",
                Category = "Test category",
                HasDelivery = true,
                ContactEmail = "Test@example.com",
                ContactNumber = "9910099100",
                Address = new Address
                {
                    City = "Test city",
                    Street = "Test street",
                    PostalCode = "122110"
                }
            };

            //act
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            //assert
            Xunit.Assert.NotNull(restaurantDto);
            Xunit.Assert.Equal(restaurant.Id, restaurantDto.Id);
            Xunit.Assert.Equal(restaurant.Name, restaurantDto.Name);
        }
    }
}