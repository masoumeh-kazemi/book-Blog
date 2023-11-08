using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rap_Blog.CoreLayer.DTOs.Posts;
using Rap_Blog.CoreLayer.Services.Categories;
using Rap_Blog.CoreLayer.Services.Posts;
using Rap_Blog.CoreLayer.Utilities;
using Rap_Blog.Web.Areas.Admin.Models.Posts;

namespace Rap_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        public PostController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }
        public IActionResult Index(string title="", string categorySlug="", int pageId=1)
        {
            var post = _postService.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                Title = title,
                PageId = pageId,
                Take = 2
            });
            return View(post);
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CreatePostViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _postService.CreatePost(new CreatePostDto()
            {
                CategoryId = createViewModel.CategoryId,
                SubCategoryId = createViewModel.SubCategoryId == 0 ? null : createViewModel.SubCategoryId,
                Title = createViewModel.Title,
                Description = createViewModel.Description,
                Slug = createViewModel.Slug,
                ImageFile = createViewModel.ImageFile,
                IsSpecial = createViewModel.IsSpecial,
                UserId = User.GetUserId()

            });
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            var model = new EditPostViewModel()
            {
                Id = id,
                CategoryId = post.CategoryId,
                SubCategoryId = post.SubcategoryId,
                Title = post.Title,
                Description = post.Description,
                Slug = post.Slug,
                IsSpecial = post.IsSpecial
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditPostViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {

                return View(editViewModel);
            }
            var result = _postService.EditPost(new EditPostDto()
            {
                Id = id,
                Title = editViewModel.Title,
                Description = editViewModel.Description,
                Slug = editViewModel.Slug,
                ImageFile = editViewModel.ImageFile,
                IsSpecial = editViewModel.IsSpecial,
                CategoryId = editViewModel.CategoryId,
                SubCategoryId = editViewModel.SubCategoryId == 0 ? null : editViewModel.SubCategoryId,
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editViewModel.Slug), result.Message);
                return View(editViewModel);

            }
            return RedirectToAction("Index");
        }
    }
}
