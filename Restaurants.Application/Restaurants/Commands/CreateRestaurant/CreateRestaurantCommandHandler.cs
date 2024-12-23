﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Respositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("{UserName}, {UserId} is Creating New restaurant {@Request}", currentUser.Email, currentUser.Id, request);
            
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;

            int id = await restaurantRepository.CreateAsync(restaurant);
            return id;
        }
    }
}
