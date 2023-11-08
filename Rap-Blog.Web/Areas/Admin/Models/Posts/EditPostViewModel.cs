using System.ComponentModel.DataAnnotations;

namespace Rap_Blog.Web.Areas.Admin.Models.Posts;

public class EditPostViewModel
{
    public int Id { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    [UIHint("Ckeditor4")]
    public string Description { get; set; }

    [Display(Name = "Slug")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public string Slug { get; set; }

    [Display(Name = "پست ویژه")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public bool IsSpecial { get; set; }

    [Display(Name = "انتخاب دسته بندی")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public int CategoryId { get; set; }

    [Display(Name = "انتخاب زیردسته")]
    [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
    public int? SubCategoryId { get; set; }

    [Display(Name = "عکس")]
    public IFormFile? ImageFile { get; set; }

}