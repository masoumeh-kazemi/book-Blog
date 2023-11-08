using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rap_Blog.DataLayer.Entities;

public class PostComment :  BaseEntity
{
    public string Text { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }


    #region Relations
    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("PostId")]
    public Post Post { get; set; }
    #endregion
}