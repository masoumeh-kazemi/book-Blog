using Microsoft.AspNetCore.Mvc;
using Rap_Blog.CoreLayer.Services.Posts;
using Rap_Blog.Web.Areas.UserPannel.Models.Posts;

namespace Rap_Blog.Web.Areas.UserPannel.Conrollers
{
    [Area("UserPannel")]

    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Route("/UserPannel/Post/{slug}")]

        public IActionResult Index(string slug)
        {
            var post = _postService.GetPostBySlug(slug);
            var model = new PostViewModel()
            {
                Post = post
            };
            return View(model);
        }
    }
}
