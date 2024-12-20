using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantQuery query)
        {
            var restaurants = await mediator.Send(query);
            //var restaurants = await restaurantsService.GetAllRestaurants();
            return Ok(restaurants);
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            //var restaurant = await restaurantsService.GetById(id);
            //if(restaurant == null)
            //{
            //    return NotFound();
            //}

            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantDto)
        {
            int id = await mediator.Send(createRestaurantDto);
            //int id = await restaurantsService.Create(createRestaurantDto);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            //var isUpdated = await mediator.Send(command);
            //if (isUpdated)
            //{
            //    return NoContent();
            //}
            return NotFound();

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            //var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));
            ////var restaurant = await restaurantsService.GetById(id);
            //if (isDeleted)
            //{
            //    return NoContent();
            //}
            await mediator.Send(new DeleteRestaurantCommand(id));   

            return NotFound();
        }

        [HttpPost("{id}/logo")]
        public async Task<IActionResult> UploadLogo([FromRoute] int id, IFormFile file)
        {
            using var stream = file.OpenReadStream();

            var command = new UploadRestaurantLogoCommand()
            {
                RestaurantId = id,
                FileName = file.FileName,
                File = stream
            };

            await mediator.Send(command);
            return NoContent();
        }

    }
}
