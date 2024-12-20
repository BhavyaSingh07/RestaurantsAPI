using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Respositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository, IBlobStorageService blobStorageService) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Getting restaurant by {request.id}");
            var restaurant = await restaurantRepository.GetByIdAsync(request.id)
                ?? throw new NotFoundException(nameof(Restaurant), request.id.ToString());
            var resdto = mapper.Map<RestaurantDto>(restaurant);
            //var resdto = RestaurantDto.FromEntity(restaurant);

            resdto.LogoSasUrl = blobStorageService.GetBlobSas(restaurant.LogoUrl);

            return resdto;
        }
    }
}
