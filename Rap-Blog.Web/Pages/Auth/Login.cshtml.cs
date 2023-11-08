using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rap_Blog.CoreLayer.DTOs.Users;
using Rap_Blog.CoreLayer.Services.Users;

namespace Rap_Blog.Web.Pages.Auth
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        #region Properties
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string Password { get; set; }
        #endregion

        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            var user = _userService.UserLogin(new UserLoginDto()
            {
                Password = Password,
                UserName = UserName
            });
            if (user == null)
            {
                ModelState.AddModelError("UserName" , "کاربری با این مشخصات یافت نشد");
                return Page();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim("Test","Test"),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            //var properties = new AuthenticationProperties()
            //{
            //    IsPersistent = true
            //};
            HttpContext.SignInAsync(claimPrincipal);


            return RedirectToPage("../Index");
        }
    }
}
