using System.Security.Claims;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //arrange
            var dateOfBirth = new DateOnly(1990, 1, 1);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Email, "test@example.com"),
                new(ClaimTypes.Role, UserRoles.Admin),
                new(ClaimTypes.Role, UserRoles.User),
                new("Nationality", "Indian"),
                new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user,
            });

            var userContext = new UserContext(httpContextAccessorMock.Object);



            //act
            var currentUser = userContext.GetCurrentUser();

            //assert
            Xunit.Assert.NotNull(currentUser);
            Xunit.Assert.Equal("1", currentUser.Id);
            Xunit.Assert.Equal("test@example.com", currentUser.Email);
            Xunit.Assert.Equal("Indian", currentUser.Nationality);
            Xunit.Assert.Equal(dateOfBirth, currentUser.DateOfBirth);
            Xunit.Assert.Contains(UserRoles.Admin, currentUser.Roles);
        }

    }
}
