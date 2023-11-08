using System.Security.AccessControl;
using Rap_Blog.CoreLayer.DTOs.Users;
using Rap_Blog.DataLayer.Entities;

namespace Rap_Blog.CoreLayer.Mapper;

public class UserMapper
{
    public static UserDto MapToDto(User user)
    {
        return new UserDto()
        {
            FullName = user.FullName,
            Password = user.Password,
            UserName = user.UserName,
        };
    }
}