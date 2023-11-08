namespace Rap_Blog.DataLayer.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public bool IsDelete { get; set; }
}