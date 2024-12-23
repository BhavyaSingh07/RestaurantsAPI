﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Respositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Deleting restaurant with id: {request.Id}");
            var restaurant = await restaurantRepository.GetByIdAsync( request.Id );
            if ( restaurant == null )
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }
            if(!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Delete))
            {
                throw new ForbidException();
            }
            await restaurantRepository.DeleteAsync(restaurant);
            //return true;
        }
    }
}
