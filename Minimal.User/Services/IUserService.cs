using Minimal.User.Models.DTO;

namespace Minimal.User.Services;
public interface IUserService
{
    GetUserDTO GetById(Guid id);
    List<GetAllUsersDTO> GetAll();
    GetUserDTO Create(CreateUserDTO user);
    GetUserDTO Update(Guid id, CreateUserDTO user);
    bool Delete(Guid id);
}

