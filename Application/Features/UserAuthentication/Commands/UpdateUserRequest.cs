using Application.Models.UserAuthentication;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.UserAuthentication.Commands
{
    public class UpdateUserRequest : IRequest<User>
    {
        public UpdateUser UserRequest;

        public UpdateUserRequest(UpdateUser userRequest)
        {
            UserRequest = userRequest;
        }
    }

    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, User>
    {
        private readonly IUserAuthRepository _userRepo;
        private readonly IMapper _mapper;

        public UpdateUserRequestHandler(IMapper mapper, IUserAuthRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<User> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                User RoleClaimUser = _mapper.Map<User>(request.UserRequest);
                User UserInDb = await _userRepo.GetUserByIdAsync(request.UserRequest.Id);
                UserInDb.Name = request.UserRequest.Name;
                UserInDb.MobileNumber = request.UserRequest.MobileNumber;
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
