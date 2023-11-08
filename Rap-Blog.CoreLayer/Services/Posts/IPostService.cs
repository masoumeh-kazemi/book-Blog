using Rap_Blog.CoreLayer.DTOs.Posts;
using Rap_Blog.CoreLayer.Utilities;

namespace Rap_Blog.CoreLayer.Services.Posts;

public interface IPostService
{
    List<PostDto> GetAllPosts();
    PostDto GetPostById(int id);
    PostDto GetPostBySlug(string slug);
    PostFilterDto GetPostByFilter(PostFilterParams filterParams);

    OperationResult CreatePost(CreatePostDto createPostDto);
    OperationResult EditPost(EditPostDto editPostDto);
    List<PostDto> GetRelatedPosts(int categoryId);
    List<PostDto> GetPopularPosts();
    List<PostDto> LatestPosts();
    void IncreaseVisit(string slug);
    public bool IsSlugExist(string slug);
}