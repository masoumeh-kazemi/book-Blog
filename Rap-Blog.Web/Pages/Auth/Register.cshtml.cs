using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rap_Blog.CoreLayer.DTOs.Users;
using Rap_Blog.CoreLayer.Services.Users;
using Rap_Blog.CoreLayer.Utilities;

namespace Rap_Blog.Web.Pages.Auth
{
    [BindProperties]
    public class RegisterModel : PageModel
    {

        #region Propeties

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید حداقل 6 کارکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string FullName { get; set; }

        #endregion

        private  readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = _userService.UserRegister(new UserRegisterDto()
            {
                FullName = FullName,
                UserName = UserName,
                Password = Password
            });

            if (user.Status != OperationResultStatus.Success)
            {
                 ModelState.AddModelError("UserName", user.Message);
                 return Page();
            }
            return RedirectToPage("Login");
        }
    }
}
