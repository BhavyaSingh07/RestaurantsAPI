using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Respositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<GetAllRestaurantQuery, PagedResult<RestaurantDto>>
    {
        public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var (restaurants, totalCount) = await restaurantRepository.GetAllMatchingAsync(request.searchPhrase, request.PageSize, request.PageNumber, request.SortBy, request.SortDirection);
            var resdto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            //var resdto = restaurants.Select(RestaurantDto.FromEntity);

            var result = new PagedResult<RestaurantDto>(resdto, totalCount, request.PageSize, request.PageNumber);

            return result;
        }
    }
}
