using Rap_Blog.DataLayer.Entities;

namespace Rap_Blog.CoreLayer.DTOs.Users;

public class UserDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }

    public int UserId { get; set; }
    public DateTime CreationDate { get; set; }
    public User.UserRole Role { get; set; }
}