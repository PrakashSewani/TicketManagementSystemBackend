using Application.Models.UserAuthentication;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.UserAuthentication.Queries
{
    public class GetUserRequest : IRequest<List<UserDto>>
    {
        public string GuidClaim;

        public GetUserRequest(string guidClaim)
        {
            GuidClaim = guidClaim;
        }
    }

    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, List<UserDto>>
    {
        private readonly IUserAuthRepository _userRepo;
        private readonly IMapper _mapper;

        public GetUserRequestHandler(IUserAuthRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        async Task<List<UserDto>> IRequestHandler<GetUserRequest, List<UserDto>>.Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepo.ValidateUserRoleClaim(Guid.Parse(request.GuidClaim));
                List<User> UsersInDb = await _userRepo.GetAllUsersAsync();
                List<UserDto> UserDtoList = new();
                foreach (var User in UsersInDb)
                {
                    UserDto tempVariable = _mapper.Map<UserDto>(User);
                    UserDtoList.Add(tempVariable);
                }
                return UserDtoList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
