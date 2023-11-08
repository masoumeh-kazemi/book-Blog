namespace Rap_Blog.DataLayer.Entities;

public class User: BaseEntity
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public UserRole Role { get; set; }

    #region Relations

    public ICollection<Post> Posts { get; set; }
    public ICollection<PostComment> Postcomments { get; set; }
    

    #endregion

    public enum  UserRole
    {
        User,
        Admin,
        Writer
    }
}