using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Respositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService(IRestaurantRepository restaurantRepository, ILogger<RestaurantsService> logger, IMapper mapper)
    {
        //public async Task<int> Create(CreateRestaurantDto createRestaurantDto)
        //{
        //    logger.LogInformation("Creating New restaurant");
        //    var restaurant = mapper.Map<Restaurant>(createRestaurantDto);
        //    int id = await restaurantRepository.CreateAsync(restaurant);
        //    return id;
        //}

        //public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        //{
        //    logger.LogInformation("Getting all restaurants");
        //    var restaurants = await restaurantRepository.GetAllAsync();
        //    var resdto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        //    //var resdto = restaurants.Select(RestaurantDto.FromEntity);

        //    return resdto!;
        //}

        //public async Task<RestaurantDto?> GetById(int id)
        //{
        //    logger.LogInformation($"Getting restaurant by {id}");
        //    var restaurant = await restaurantRepository.GetByIdAsync(id);
        //    var resdto = mapper.Map<RestaurantDto?>(restaurant);
        //    //var resdto = RestaurantDto.FromEntity(restaurant);

        //    return resdto;
        //}
    }
}
