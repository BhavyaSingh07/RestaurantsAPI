using System.Numerics;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands
{
    public class UpdateUserDetailCommandHandler(ILogger<UpdateUserDetailCommandHandler> logger, IUserContext userContext, IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailCommand>
    {
        public async Task Handle(UpdateUserDetailCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Updating user: {UserId} with {@Request}", user!.Id, request);

            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

            if (dbUser == null)
            {
                throw new NotFoundException(nameof(User), user!.Id);
            }
            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateOfBirth;

            await userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
