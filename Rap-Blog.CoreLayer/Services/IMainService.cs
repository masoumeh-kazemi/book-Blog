using Microsoft.EntityFrameworkCore;
using Rap_Blog.CoreLayer.DTOs;
using Rap_Blog.CoreLayer.Mapper;
using Rap_Blog.DataLayer.Context;

namespace Rap_Blog.CoreLayer.Services;

public interface IMainService
{
    MainPageDto GetData();
}

public class MainService : IMainService
{
    private readonly BlogContext _context;

    public MainService(BlogContext context)
    {
        _context = context;
    }
    public MainPageDto GetData()
    {

        var post = _context.Posts.OrderByDescending(f => f.CreationDate);
        var latestPost = post.Take(4)
            .Include(f=>f.User)
            .Include(f=>f.Category)
            .Select(p => PostMapper.MapToDto(p)).ToList();

        var spacialPost = post.Where(f=>f.IsSpecial).Take(4)
            .Include(f => f.User)
            .Include(f => f.Category)
            .Select(p => PostMapper.MapToDto(p)).ToList();

        var categories = _context.Categories.Select(category => new MainPageCategoryDto()
        {
            Slug = category.Slug,
            Title = category.Title,
            PostChild = category.Posts.Count + category.SubPost.Count,
            IsMainCategory = category.ParentId == null
        }).ToList();
        return new MainPageDto
        {
            LatestPosts = latestPost,
            SpacialPosts = spacialPost,
            Categories = categories
        };
    }
}