using Microsoft.AspNetCore.Http;
using Rap_Blog.CoreLayer.Utilities;

namespace Rap_Blog.CoreLayer.Services.FileManager;

public class FileManager:IFileManager
{
    public string SaveImageAndReturnName(IFormFile file, string savePath)
    {
        var isNotImage = !ImageValidation.Validate(file.FileName);
        if (isNotImage)
            throw new Exception();
       return SaveFileAndReturnName(file, savePath);
    
    }

    public string SaveFileAndReturnName(IFormFile file, string savePath)
    {
        if (file == null)
            throw new Exception("File is null");
        var fileName = $"{Guid.NewGuid()}{file.FileName}";
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/","\\"));
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fullPath = Path.Combine(folderPath, fileName);
        using var stream = new FileStream(fullPath, FileMode.Create);
        file.CopyTo(stream);
        return fileName;
    }
}