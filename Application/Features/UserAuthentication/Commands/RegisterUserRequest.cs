using Application.Models.UserAuthentication;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.UserAuthentication.Commands
{
    public class RegisterUserRequest : IRequest<User>
    {
        public RegisterUser UserRequest;

        public RegisterUserRequest(RegisterUser userRequest)
        {
            UserRequest = userRequest;
        }
    }

    public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, User>
    {
        private readonly IUserAuthRepository _userRepo;
        private readonly IMapper _mapper;

        public RegisterUserRequestHandler(IUserAuthRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<User> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                User RequestUser = _mapper.Map<User>(request.UserRequest);
                User Response = await _userRepo.RegisterUserAsync(RequestUser);
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
