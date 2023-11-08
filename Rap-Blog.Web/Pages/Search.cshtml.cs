 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rap_Blog.CoreLayer.DTOs.Posts;
using Rap_Blog.CoreLayer.Services.Posts;

namespace Rap_Blog.Web.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IPostService _postService;

        public SearchModel(IPostService postService)
        {
            _postService = postService;
        }
        public PostFilterDto Filter { get; set; }
        public void OnGet(int pageId = 1, string categorySlug = "", string q = null)
        {
            Filter = _postService.GetPostByFilter(new PostFilterParams()
            {
                PageId = pageId,
                CategorySlug = categorySlug,
                Take = 1,
                Title = q
            });
        }
        public IActionResult OnGetPagination(int pageId = 1, string categorySlug = "", string q = null)
        {
            var model = _postService.GetPostByFilter(new PostFilterParams()
            {
                PageId = pageId,
                CategorySlug = categorySlug,
                Take = 1,
                Title = q
            });

            return Partial("_SearchView", model);
        }

    }
}
