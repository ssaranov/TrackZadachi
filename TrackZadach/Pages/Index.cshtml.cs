using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrackZadach.Models;

namespace TrackZadach.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public IndexModel(AppContext context)
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
            if (string.IsNullOrEmpty(RegisterName)
               || string.IsNullOrEmpty(RegisterLogin)
               || string.IsNullOrEmpty(RegisterPassword)
              )
            {
                Message = "«аполните все пол€ регистрации";
                return Page();
            }
            if (_context.User.Any(u => u.Login == RegisterLogin))
            {
                Message = "ѕользователь с таким логином уже существует";
                return Page();
            }

        }







        private void LoadUser()
        {
            throw new NotImplementedException();
        }
    }
}
