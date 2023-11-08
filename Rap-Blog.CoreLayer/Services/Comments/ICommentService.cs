using Rap_Blog.CoreLayer.DTOs.PostComments;
using Rap_Blog.CoreLayer.Utilities;

namespace Rap_Blog.CoreLayer.Services.Comments;

public interface ICommentService
{
    List<PostCommentDto> GetComments(int postId);
    OperationResult CreatePostComment(CreatePostCommentDto createPostComment);

}