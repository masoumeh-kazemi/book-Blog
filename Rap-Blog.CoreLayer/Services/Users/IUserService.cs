using Rap_Blog.CoreLayer.DTOs.Users;
using Rap_Blog.CoreLayer.Utilities;
using Rap_Blog.DataLayer.Context;
using Rap_Blog.DataLayer.Entities;

namespace Rap_Blog.CoreLayer.Services.Users;

public interface IUserService
{
    OperationResult UserRegister(UserRegisterDto userRegisterDto);
    UserDto UserLogin(UserLoginDto userLoginDto);
}

public class UserService : IUserService
{
    private readonly BlogContext _context;

    public UserService(BlogContext context)
    {
        _context = context;
    }
    public OperationResult UserRegister(UserRegisterDto userRegisterDto)
    {
        var isUserNameExist = _context.Users.Any(f => f.UserName == userRegisterDto.UserName);
        if(isUserNameExist)
            return OperationResult.Error("نام کاربری تکراری است");

        var passwordHash = userRegisterDto.Password.EncodeToMd5();
        var user = new User()
        {
            UserName = userRegisterDto.UserName,
            Password = passwordHash,
            FullName = userRegisterDto.FullName 
        };

        _context.Add(user);
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public UserDto UserLogin(UserLoginDto userLoginDto)
    {
        var passwordHashed = userLoginDto.Password.EncodeToMd5();
        var user = _context.Users.FirstOrDefault(f =>
            f.UserName == userLoginDto.UserName && f.Password == passwordHashed);
        
        return new UserDto()
        {
            UserId = user.Id,
            CreationDate = user.CreationDate,
            Role = user.Role,
            FullName = user.FullName,
            UserName = user.UserName
        };
    }
}