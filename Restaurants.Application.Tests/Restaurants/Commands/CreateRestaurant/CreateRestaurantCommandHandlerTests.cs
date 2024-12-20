using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Moq;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Restaurants.Domain.Respositories;
using Restaurants.Domain.Entities;
using Restaurants.Application.Users;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ResturnsCreatedRestaurantId()
        {
            //arrange
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

            var mapperMock = new Mock<IMapper>();

            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();
            mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

            var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
            var userContextMock = new Mock<IUserContext>();

            restaurantRepositoryMock.Setup(op => op.CreateAsync(It.IsAny<Restaurant>())).ReturnsAsync(1);

            var currentUser = new CurrentUser("owner-id", "test@example.com", [], null, null);
            userContextMock.Setup(t => t.GetCurrentUser()).Returns(currentUser);


            var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object, mapperMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);

            //act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //assert
            Xunit.Assert.Equal(1, result);
            Xunit.Assert.Equal("owner-id", restaurant.OwnerId);
            restaurantRepositoryMock.Verify(r => r.CreateAsync(restaurant), Times.Once);
        }
    }
}