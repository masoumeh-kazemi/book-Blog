using System.Security.AccessControl;
using Rap_Blog.CoreLayer.DTOs.Categories;
using Rap_Blog.CoreLayer.DTOs.Users;

namespace Rap_Blog.CoreLayer.DTOs.Posts;

public class PostDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string ImageName { get; set; }
    public int Visit { get; set; }
    public DateTime CreationDate { get; set; }
    public int UserId { get; set; }
    public bool IsSpecial { get; set; }
    public int CategoryId { get; set; }
    public int? SubcategoryId { get; set; }
    public CategoryDto Category { get; set; }
    public CategoryDto? SubCategory { get; set; }
    public UserDto User { get; set; }
}