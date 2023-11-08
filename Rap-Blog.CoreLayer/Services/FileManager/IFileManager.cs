using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace Rap_Blog.CoreLayer.Services.FileManager;

public interface IFileManager
{
    string SaveImageAndReturnName(IFormFile file, string savePath);

    string SaveFileAndReturnName(IFormFile file, string savePath);
}