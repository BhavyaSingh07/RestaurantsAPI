using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? LogoSasUrl { get; set; }
        public List<DishDto> Dishes { get; set; } = [];

        //public static RestaurantDto? FromEntity(Restaurant? restaurant)
        //{
        //    if(restaurant == null)
        //    {
        //        return null;
        //    }
        //    return new RestaurantDto()
        //    {
        //        Category = restaurant.Category,
        //        Description = restaurant.Description,
        //        Name = restaurant.Name,
        //        Id = restaurant.Id,
        //        HasDelivery = restaurant.HasDelivery,
        //        City = restaurant.Address?.City,
        //        Street = restaurant.Address?.Street,
        //        PostalCode = restaurant.Address?.PostalCode,
        //        Dishes = restaurant.Dishes.Select(DishDto.FromEntity).ToList()
        //    };
        //}
    }
}
