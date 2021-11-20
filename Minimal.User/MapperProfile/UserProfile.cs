using Minimal.User.Models.DTO;
using AutoMapper;
namespace Minimal.User.MapperProfile
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<Models.Entity.User, CreateUserDTO>();
            CreateMap<CreateUserDTO, Models.Entity.User>();
            CreateMap<Models.Entity.User, GetUserDTO>();
            CreateMap<GetUserDTO, Models.Entity.User>();
            CreateMap<Models.Entity.User, GetAllUsersDTO>();
            CreateMap<GetAllUsersDTO,Models.Entity.User>();
        }
    }
}
