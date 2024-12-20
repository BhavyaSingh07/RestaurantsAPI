using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        //TestMethod_Scenario_ExpectedResult
        //[Fact()]
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void isInRole_WithMathcingRole_ShouldReturnTrue(string roleName)
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@example.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var isInRole = currentUser.isInRole(roleName);

            //assert
            Xunit.Assert.True(isInRole);
        }


        [Fact()]
        public void isInRole_WithNoMathcingRole_ShouldReturnFalse()
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@example.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var isInRole = currentUser.isInRole(UserRoles.Owner);

            //assert
            Xunit.Assert.False(isInRole);
        }

        [Fact()]
        public void isInRole_WithNoMathcingRoleCase_ShouldReturnFalse()
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@example.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var isInRole = currentUser.isInRole(UserRoles.Admin.ToLower());

            //assert
            Xunit.Assert.False(isInRole);
        }
    }
}