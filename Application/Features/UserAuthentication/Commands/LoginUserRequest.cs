using Application.Models.UserAuthentication;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.UserAuthentication.Commands
{
    public class LoginUserRequest : IRequest<LoggedInUser>
    {
        public LoginUser UserRequest;

        public LoginUserRequest(LoginUser userRequest)
        {
            UserRequest = userRequest;
        }
    }

    public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, LoggedInUser>
    {
        private readonly IUserAuthRepository _userRepo;
        private readonly IMapper _mapper;

        public LoginUserRequestHandler(IUserAuthRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<LoggedInUser> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                LoggedInUser Response = await _userRepo.LoginUserAsync(request.UserRequest.Email, request.UserRequest.Password);
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
