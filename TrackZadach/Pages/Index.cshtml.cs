using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrackZadach.Data;
using TrackZadach.Models;

namespace TrackZadach.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public IndexModel(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            
        }
        [BindProperty]
        public string RegisterName { get; set; } = string.Empty;
        [BindProperty]
        public string RegisterLogin { get; set; } = string.Empty;
        [BindProperty]
        public string RegisterPassword { get; set; } = string.Empty;
        [BindProperty]
        public string RegistrRepeatPassword { get; set; } = string.Empty;
        [BindProperty]
        public string LoginLogin { get; set; } = string.Empty;
        [BindProperty]
        public string LoginPassword { get; set; } = string.Empty;

        public bool IsAuthorized { get; set; }
        public string CurrentUserName { get; set; } = string.Empty;
        public string CurrentUserLogin { get; set; } = string.Empty;
        public int CurrentUserAge { get; set; }
        public string Message { get; set; } = string.Empty;


        public void OnGet()
        {
            LoadUser();
        }

        public IActionResult OnPostRegister()
        {
            LoadUser();
            if (string.IsNullOrEmpty(LoginLogin) || string.IsNullOrEmpty(LoginPassword))
            {
                Message = "Введите логин и пароль.";
                return Page();
            }
            var user = _context.Users.FirstOrDefault(u => u.Login == LoginLogin);

            if (user == null)
            {
                Message = "Неверный логин или праоль.";
                return Page();
            }
            var res = _passwordHasher.VerifyHashedPassword(
                    user,
                    user.HashPassword,
                    LoginPassword

                );

            if (res == PasswordVerificationResult.Failed)
            {
                Message = "Не верный логин или пароль";
                return Page();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            //HttpContext.Session.SetString("UserName", user.Name);
            //HttpContext.Session.SetString("UserLogin", user.Login);

            return RedirectToPage();
        }
        private void LoadUser()
        {

        }
    }
}
