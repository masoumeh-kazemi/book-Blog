using System.ComponentModel.DataAnnotations.Schema;

namespace Rap_Blog.DataLayer.Entities;

public class Post : BaseEntity
{
    public string Description { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public int Visit { get; set; }
    public string ImageName { get; set; }
    public int UserId { get; set; }
    public int  CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public bool IsSpecial { get; set; }

    #region Relations

    [ForeignKey("UserId")]
    public User User { get; set; }


    [ForeignKey("CategoryId")]
    public Category Category { get; set; }


    [ForeignKey("SubCategoryId")]
    public Category? SubCategory { get; set; }

    public ICollection<PostComment> PostComments { get; set; }
    #endregion
}