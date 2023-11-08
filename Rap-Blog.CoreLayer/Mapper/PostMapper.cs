using Rap_Blog.CoreLayer.DTOs.Posts;
using Rap_Blog.DataLayer.Entities;

namespace Rap_Blog.CoreLayer.Mapper;

public class PostMapper
{
    public static PostDto MapToDto(Post post)
    {
        return new PostDto()
        {
            Id = post.Id,
            CreationDate = post.CreationDate,
            Visit = post.Visit,
            Slug = post.Slug,
            Title = post.Title,
            Description = post.Description,
            ImageName = post.ImageName,
            IsSpecial = post.IsSpecial,
            CategoryId = post.CategoryId,
            SubcategoryId = post.SubCategoryId == null? null : post.SubCategoryId,
            UserId = post.UserId,
            SubCategory = post.SubCategory == null ? null : CategoryMapper.MapToDto(post.SubCategory),
            Category = CategoryMapper.MapToDto(post.Category),
            User = UserMapper.MapToDto(post.User),


        };
    }


    public static List<PostDto> MapToListDto(ICollection<Post> posts)
    {
        var postDtoList = new List<PostDto>();
        foreach (var post in posts)
        {
            postDtoList.Add(MapToDto(post));
        }

        return postDtoList;
    }
}