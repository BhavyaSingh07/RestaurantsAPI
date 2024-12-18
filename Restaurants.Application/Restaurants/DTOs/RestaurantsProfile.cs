using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>().ReverseMap();

            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, op => op.MapFrom(src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }))
                .ReverseMap();

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.City, op => op.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.PostalCode, op => op.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(d => d.Street, op => op.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.Dishes, op => op.MapFrom(src => src.Dishes))
                .ReverseMap();
        }

    }
}
