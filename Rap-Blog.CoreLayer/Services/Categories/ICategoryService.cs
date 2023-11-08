using Rap_Blog.CoreLayer.DTOs.Categories;
using Rap_Blog.CoreLayer.Utilities;

namespace Rap_Blog.CoreLayer.Services.Categories;

public interface ICategoryService
{
    List<CategoryDto> GetAllCategories();
    CategoryDto GetCategoryById(int id);
    CategoryDto GetCategoryBySlug(string slug);
    List<CategoryDto> GetChildCategories(int parentId);
    OperationResult CreateCategory(CreateCategoryDto createCategoryDto);
    OperationResult EditCategory(EditCategoryDto editDto);

    bool IsSlugExist(string slug);
}