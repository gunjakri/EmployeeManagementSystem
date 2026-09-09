using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // New users get Employee role
                await _userManager.AddToRoleAsync(user,"Employee");

                await _signInManager.SignInAsync(user,isPersistent: false);

                return RedirectToAction("Index","Employee");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }

            return View();
        }


        // Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email,string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email,password,false,false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index","Employee");
            }

            ViewBag.Error = "Invalid email or password";

            return View();
        }


        // Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }


        // Make Admin
        public async Task<IActionResult> MakeAdmin()
        {
            var user = await _userManager.FindByEmailAsync("@gmail.com");

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            if (!result.Succeeded)
            {
                return Content("Failed to assign Admin role");
            }

            return Content("User is now Admin");
        }
    }
}