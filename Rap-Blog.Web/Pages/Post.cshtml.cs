using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rap_Blog.CoreLayer.DTOs.PostComments;
using Rap_Blog.CoreLayer.DTOs.Posts;
using Rap_Blog.CoreLayer.Services.Comments;
using Rap_Blog.CoreLayer.Services.Posts;
using Rap_Blog.CoreLayer.Utilities;
using System.Xml.Linq;

namespace Rap_Blog.Web.Pages
{
    public class PostModel : PageModel
    {
        #region Propeeties
        public PostDto Post { get; set; }
        public List<PostDto> RelatedPosts { get; set; }
        public List<PostCommentDto> PostComments { get; set; }

        [BindProperty]
        public int PostId { get; set; }

        [BindProperty]
        public string Text { get; set; }

        #endregion

        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        public PostModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }
        public IActionResult OnGet(string slug)
        {
            Post = _postService.GetPostBySlug(slug);
            PostComments = _commentService.GetComments(Post.Id);
            RelatedPosts = _postService.GetRelatedPosts(Post.CategoryId);
            _postService.IncreaseVisit(slug);


            return Page();
        }

        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("Post", new { slug = slug });

            if (!ModelState.IsValid)
            {
                Post = _postService.GetPostBySlug(slug);
                PostComments = _commentService.GetComments(Post.Id);
                RelatedPosts = _postService.GetRelatedPosts(Post.CategoryId);
                return Page();
            }

            _commentService.CreatePostComment(new CreatePostCommentDto()
            {
                PostId = PostId,
                Text = Text,
                UserId = User.GetUserId()
            });

            return RedirectToPage("Post", new { slug = slug });
        }
    }
}
