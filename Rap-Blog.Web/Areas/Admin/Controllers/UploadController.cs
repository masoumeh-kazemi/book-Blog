using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rap_Blog.CoreLayer.Services.FileManager;
using Rap_Blog.CoreLayer.Utilities;

namespace Rap_Blog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UploadController : Controller
    {
        private readonly IFileManager _fileManger;

        public UploadController(IFileManager fileManger)
        {
            _fileManger = fileManger;
        }

        [Route("/Upload/Article")]
        //[Authorize]
        public IActionResult UploadArticleImage(IFormFile upload)
        {
            if (upload == null)
                BadRequest();

            var imageName = _fileManger.SaveFileAndReturnName(upload, Directories.PostContentImage);
            return Json(new { Uploaded = true, url = Directories.GetPostContentImage(imageName) });

        }
    }
}
