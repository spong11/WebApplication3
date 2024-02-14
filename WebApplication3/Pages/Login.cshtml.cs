using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;
using System.Threading.Tasks; 

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager; // Define userManager here

        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) // Inject userManager here
        {
            this.signInManager = signInManager;
            this.userManager = userManager; // Assign injected userManager to the local variable
        }

        [BindProperty]
        public Login LModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(LModel.Email);

                if (user != null && !await userManager.IsLockedOutAsync(user))
                {
                    var result = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
                        LModel.RememberMe, false);

                    if (result.Succeeded)
                    {
                        // Reset lockout count on successful login
                        await userManager.ResetAccessFailedCountAsync(user);
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        // Increment access failed count and lockout if necessary
                        await userManager.AccessFailedAsync(user);

                        if (await userManager.IsLockedOutAsync(user))
                        {
                            // Implement account lockout logic here
                        }

                        ModelState.AddModelError("", "Username or Password incorrect");
                    }
                }
                else
                {
                    // Handle account locked or not found
                    ModelState.AddModelError("", "Account not found or locked.");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await signInManager.SignOutAsync();
            HttpContext.Session.Clear(); // Clear session
            return RedirectToPage("/Login");
        }

        public IActionResult RedirectToHome()
        {
            return RedirectToPage("Index");
        }

        public async Task LogActivity(string activity)
        {
            // Log user activity to the database or other logging mechanism
            // Example: Save user activity to a database table
        }
    }
}

