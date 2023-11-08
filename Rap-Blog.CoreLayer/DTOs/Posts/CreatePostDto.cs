using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;

namespace Rap_Blog.CoreLayer.DTOs.Posts;

public class CreatePostDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public int UserId { get; set; }
    public IFormFile ImageFile { get; set; }
    public bool IsSpecial { get; set; }

}