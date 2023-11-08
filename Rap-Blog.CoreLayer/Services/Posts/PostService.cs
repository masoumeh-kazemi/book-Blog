using Microsoft.EntityFrameworkCore;
using Rap_Blog.CoreLayer.DTOs.Posts;
using Rap_Blog.CoreLayer.Mapper;
using Rap_Blog.CoreLayer.Services.FileManager;
using Rap_Blog.CoreLayer.Utilities;
using Rap_Blog.DataLayer.Context;
using Rap_Blog.DataLayer.Entities;

namespace Rap_Blog.CoreLayer.Services.Posts;

public class PostService : IPostService
{
    private readonly BlogContext _context;
    private readonly IFileManager _fileManager;

    public PostService(BlogContext context, IFileManager fileManager)
    {
        _context = context;
        _fileManager = fileManager;
    }
    public List<PostDto> GetAllPosts()
    {
        var posts = _context.Posts
            .Include(post => post.Category)
            .Include(post => post.User)
            .Select(post => new PostDto()
            {
                Id = post.Id,
                CreationDate = post.CreationDate,
                Slug = post.Slug,
                Title = post.Title,
                Description = post.Description,
                ImageName = post.ImageName,
                CategoryId = post.CategoryId,
                UserId = post.UserId,
                Category = CategoryMapper.MapToDto(post.Category),
                User = UserMapper.MapToDto(post.User)

            }).ToList();

        return posts;
    }

    public PostDto GetPostById(int id)
    {
        var post = _context.Posts
            .Include(post => post.Category)
            .Include(post => post.User)
            .FirstOrDefault(post => post.Id == id);

        return PostMapper.MapToDto(post);
    }

    public PostDto GetPostBySlug(string slug)
    {
        var post = _context.Posts
            .Include(post => post.Category)
            .Include(post=>post.SubCategory)
            .Include(post => post.User)
            .FirstOrDefault(f => f.Slug == slug);
        return PostMapper.MapToDto(post);
    }

    public PostFilterDto GetPostByFilter(PostFilterParams filterParams)
    {
        var results = _context.Posts.OrderByDescending(f=>f.CreationDate)
            .Include(post=> post.Category)
            .Include(post=>post.SubCategory)
            .Include(post=>post.User)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filterParams.CategorySlug))
            results = results.Where(f => f.Category.Slug == filterParams.CategorySlug || f.SubCategory.Slug==filterParams.CategorySlug);

        if (!string.IsNullOrEmpty(filterParams.Title))
            results = results.Where(f => f.Title.Contains(filterParams.Title));

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var pageCount = results.Count()/ filterParams.Take;
        var model = new PostFilterDto()
        {
            FilterParams = filterParams,
            Posts = results.Skip(skip).Take(filterParams.Take).Select(post => PostMapper.MapToDto(post)).ToList(),
        };
        
        model.GeneratePaging(results, filterParams.Take,filterParams.PageId);
        return model;

    }

    public OperationResult CreatePost(CreatePostDto createPostDto)
    {
        var imageName = _fileManager.SaveImageAndReturnName(createPostDto.ImageFile, Directories.PostImage);
        if(IsSlugExist(createPostDto.Slug.ToSlug()))
            return OperationResult.Error("Slug تکراری است");
        var post = new Post()
        {
            CategoryId = createPostDto.CategoryId,
            SubCategoryId = createPostDto.SubCategoryId,
            Title = createPostDto.Title,
            Slug = createPostDto.Slug.ToSlug(),
            Description = createPostDto.Description,
            CreationDate = DateTime.Now,
            IsDelete = false,
            ImageName = imageName,
            UserId = createPostDto.UserId,
            IsSpecial = createPostDto.IsSpecial
        };
        _context.Add(post);
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public OperationResult EditPost(EditPostDto editPostDto)
    {
        var post = _context.Posts.FirstOrDefault(f => f.Id == editPostDto.Id);
        if (post == null)
            return OperationResult.NotFound();

        if (editPostDto.ImageFile != null)
            post.ImageName = _fileManager.SaveImageAndReturnName(editPostDto.ImageFile, Directories.PostImage);
        var oldSlug = post.Slug;
        var newSlug = editPostDto.Slug.ToSlug();
        if(newSlug != oldSlug)
            if(IsSlugExist(newSlug))
                return OperationResult.Error("slug تکراری است");

        post.Title = editPostDto.Title;
        post.Slug = editPostDto.Slug.ToSlug();
        post.Description = editPostDto.Description;
        post.IsSpecial = editPostDto.IsSpecial;
        post.CategoryId = editPostDto.CategoryId;
        post.SubCategoryId = editPostDto.SubCategoryId;

        _context.SaveChanges();
        return OperationResult.Success();
    }

    public List<PostDto> GetRelatedPosts(int categoryId)
    {
        var relatedPosts = _context.Posts
            .Where(f => f.CategoryId == categoryId || f.SubCategoryId == categoryId)
            .Take(6)
            .Select(post=> new PostDto
            {
                Title = post.Title,
                ImageName = post.ImageName,
                Slug = post.Slug,
            }).ToList();
        return relatedPosts;
    }

    public List<PostDto> GetPopularPosts()
    {
        var popularPosts = _context.Posts.OrderByDescending(f=>f.Visit).Take(4).Select(post => new PostDto()
        {
            Title = post.Title,
            Slug = post.Slug,
            ImageName = post.ImageName,
            User = UserMapper.MapToDto(post.User),
            CreationDate = post.CreationDate
        }).ToList();
        return popularPosts;
    }

    public List<PostDto> LatestPosts()
    {
        var latestPost = _context.Posts
            .OrderByDescending(f=>f.CreationDate)
            .Take(4)
            .Select(post => PostMapper.MapToDto(post)).ToList();
        return latestPost;
    }

    public void IncreaseVisit(string slug)
    {
        var post = _context.Posts.FirstOrDefault(f => f.Slug == slug);
        post.Visit += 1;
        _context.SaveChanges();
    }

    public bool IsSlugExist(string slug)
    {
        return _context.Posts.Any(f => f.Slug == slug);
    }

}