using Application.Models.UserAuthentication;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.UserAuthentication.Commands
{
    public class RegisterUserRequest : IRequest<LoggedInUser>
    {
        public RegisterUser UserRequest;

        public RegisterUserRequest(RegisterUser userRequest)
        {
            UserRequest = userRequest;
        }
    }

    public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, LoggedInUser>
    {
        private readonly IUserAuthRepository _userRepo;
        private readonly IMapper _mapper;

        public RegisterUserRequestHandler(IUserAuthRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<LoggedInUser> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                User RequestUser = _mapper.Map<User>(request.UserRequest);
                User Response = await _userRepo.RegisterUserAsync(RequestUser);
                string UserToken = await _userRepo.GenerateJwtTokenAsync(Response);
                LoggedInUser User = new()
                {
                    User = Response,
                    BearerToken = UserToken
                };
                return User;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
