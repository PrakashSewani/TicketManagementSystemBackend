using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.UserAuthentication.Queries
{
    public class DeleteUserRequest : IRequest<bool>
    {
        public Guid UserId;

        public DeleteUserRequest(Guid userId)
        {
            UserId = userId;
        }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, bool>
    {
        private readonly IUserAuthRepository _userRepo;

        public DeleteUserRequestHandler(IUserAuthRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                User UserInDb = await _userRepo.GetUserByIdAsync(request.UserId);
                await _userRepo.DeleteUserAsync(UserInDb);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
