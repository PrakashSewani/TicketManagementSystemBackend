using Application.Models.UserAuthentication;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RegisterUser, User>();
            CreateMap<UpdateUser, User>();
        }
    }
}
