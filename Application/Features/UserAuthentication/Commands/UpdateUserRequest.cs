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
                User UserInDb = await _userRepo.GetUserByIdAsync(request.UserRequest.Id);
                UserInDb.Name = request.UserRequest.Name;
                UserInDb.MobileNumber = request.UserRequest.MobileNumber;
                UserInDb.Role = new Role()
                {
                    Id = Guid.Parse("6F2A960A-7D3B-4F61-881B-22EE6C319948"),
                    RoleName = "TheatreUser"
                };

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
