using System.ComponentModel.DataAnnotations;

namespace Rap_Blog.Web.Areas.Admin.Models.Posts;

public class CreatePostViewModel
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public string Title { get; set; }

    [Display(Name = "Slug")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public string Slug { get; set; }

    [Display(Name = "عکس")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    [UIHint("Ckeditor4")]
    public string Description { get; set; }

    [Display(Name = "دسته")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public int CategoryId { get; set; }

    [Display(Name = "زیر دسته")]
    public int? SubCategoryId { get; set; }

    [Display(Name="پست ویژه؟")]
    public bool IsSpecial { get; set; }

}