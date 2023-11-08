using System.ComponentModel.DataAnnotations.Schema;

namespace Rap_Blog.DataLayer.Entities;

public class Category : BaseEntity
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string MetaTag { get; set; }
    public string MetaDescription { get; set; }
    public int? ParentId { get; set; }

    #region Relations
    [InverseProperty("Category")]
    public ICollection<Post> Posts { get; set; }

    [InverseProperty("SubCategory")]
    public ICollection<Post> SubPost { get; set; }


    #endregion
}