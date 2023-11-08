using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rap_Blog.CoreLayer.DTOs;
using Rap_Blog.CoreLayer.Services;
using Rap_Blog.CoreLayer.Services.Posts;

namespace Rap_Blog.Web.Pages
{
    public class IndexModel : PageModel
    {

        #region Properties

        public MainPageDto MainPageData { get; set; }

        #endregion

        private readonly ILogger<IndexModel> _logger;
        private readonly IPostService _postService;
        private readonly IMainService _mainService;
        public IndexModel(ILogger<IndexModel> logger, IPostService postService, IMainService mainService)
        {
            _logger = logger;
            _postService = postService;
            _mainService = mainService;
        }

        public void OnGet()
        {
            MainPageData = _mainService.GetData();
        }

        public IActionResult OnGetPopularPost()
        {
            return Partial("_PopularPosts", _postService.GetPopularPosts());
        }

    }
}