using Microsoft.EntityFrameworkCore;
using Rap_Blog.CoreLayer.DTOs.Categories;
using Rap_Blog.CoreLayer.Mapper;
using Rap_Blog.CoreLayer.Utilities;
using Rap_Blog.DataLayer.Context;
using Rap_Blog.DataLayer.Entities;

namespace Rap_Blog.CoreLayer.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly BlogContext _context;

    public CategoryService(BlogContext context)
    {
        _context = context;
    }
    public List<CategoryDto> GetAllCategories()
    {
        var category = _context.Categories
            .OrderByDescending(f=>f.CreationDate)
            .Include(f=>f.Posts)
            .Select(category => CategoryMapper.MapToDto(category)).ToList();
        return category;
    }

    public CategoryDto GetCategoryBySlug(string slug)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Slug==slug);
        if (category == null)
        {
            return null;
        }
        return CategoryMapper.MapToDto(category);
    }

    public List<CategoryDto> GetChildCategories(int parentId)
    {
        var childCategories = _context.Categories.Where(f => f.ParentId == parentId)
            .Select(category => new CategoryDto()
            {
                Id = category.Id,
                Title = category.Title,
                Slug = category.Slug,
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
            }).ToList();
        return childCategories;
    }

    public OperationResult CreateCategory(CreateCategoryDto createCategoryDto)
    {
        if(IsSlugExist(createCategoryDto.Slug.ToSlug()))
            return OperationResult.Error("Slug تکراری است");

        var category = new Category()
        {
            Title = createCategoryDto.Title,
            Slug = createCategoryDto.Slug.ToSlug(),
            MetaDescription = createCategoryDto.MetaDescription,
            MetaTag = createCategoryDto.MetaTag,
            ParentId = createCategoryDto.ParentId
        };
        _context.Categories.Add(category);
        _context.SaveChanges();
        return OperationResult.Success();

    }

    public CategoryDto GetCategoryById(int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
            return null;

        return CategoryMapper.MapToDto(category);
    }

    public OperationResult EditCategory(EditCategoryDto editDto)
    {
        var category = _context.Categories.FirstOrDefault(f => f.Id == editDto.Id);
        if (category == null)
            return null;

         var oldSlug = category.Slug;
         var newSlug = editDto.Slug.ToSlug();
         if (oldSlug != newSlug)
             if(IsSlugExist(newSlug.ToSlug()))
                 return OperationResult.Error("slug تکراری است");

         CategoryMapper.EditToCategory(editDto, category);
        _context.SaveChanges();
        return OperationResult.Success();

    }

    public bool IsSlugExist(string slug)
    {

        return _context.Categories.Any(f => f.Slug == slug);
    }
}