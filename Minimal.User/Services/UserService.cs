using AutoMapper;
using Minimal.User.Models.DTO;
using Minimal.User.Models.Entity;
using System.Linq;

namespace Minimal.User.Services;

public class UserService : IUserService
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;

    public UserService(UserDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GetUserDTO Create(CreateUserDTO userDto)
    {
        Models.Entity.User user = new Models.Entity.User();
        user = _mapper.Map<Models.Entity.User>(userDto);
        _context.Users.Add(user);
        _context.SaveChanges();
        return GetById(user.Id);
    }

    public bool Delete(Guid id)
    {
        Models.Entity.User user = _context.Users.Where(a => a.Id == id).FirstOrDefault();
        if (user == null)
        {
            throw new Exception("User Not Found!");
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }

    public List<GetAllUsersDTO> GetAll()
    {
        List<GetAllUsersDTO> list = new List<GetAllUsersDTO>();
        List<Models.Entity.User> users = _context.Users.ToList();
        list = _mapper.Map<List<Models.Entity.User>, List<GetAllUsersDTO>> (users);
        return list;
    }

    public GetUserDTO GetById(Guid id)
    {
        Models.Entity.User user = null;
        user = _context.Users.Where(a => a.Id == id).FirstOrDefault();
        if (user == null)
        {
            throw new Exception("User Not Found!");
        }

        GetUserDTO result = _mapper.Map<GetUserDTO>(user);
        return result;
    }

    public GetUserDTO Update(Guid id, CreateUserDTO userDto)
    {
        Models.Entity.User user = _context.Users.Where(a => a.Id == id).FirstOrDefault();
        if (user == null)
        {
            throw new Exception("User Not Found!");
        }

        user.Address = userDto.Address;
        user.Email = userDto.Email;
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        
        _context.Users.Update(user);
        _context.SaveChanges();

        return GetById(id);
    }
}

