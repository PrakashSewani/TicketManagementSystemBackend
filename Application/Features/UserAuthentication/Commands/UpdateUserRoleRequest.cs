using Application.Models.UserAuthentication;
using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.UserAuthentication.Commands
{
    public class UpdateUserRoleRequest : IRequest<User>
    {
        public UpdateUserRole UpdateUser;
        public Guid UserIdClaim;

        public UpdateUserRoleRequest(UpdateUserRole updateUser, Guid userIdClaim)
        {
            UpdateUser = updateUser;
            UserIdClaim = userIdClaim;
        }
    }

    public class UpdateUserRoleRequestHandler : IRequestHandler<UpdateUserRoleRequest, User>
    {
        private readonly IUserAuthRepository _userRepo;

        public UpdateUserRoleRequestHandler(IUserAuthRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> Handle(UpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepo.ValidateUserRoleClaim(request.UserIdClaim);
                User UserInDb = await _userRepo.GetUserByIdAsync(request.UpdateUser.Id);
                UserInDb.RoleId = request.UpdateUser.RoleId;
                User Response = await _userRepo.UpdateUserAsync(UserInDb);

                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
