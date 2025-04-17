using HR_Tool.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Route("Admin-Login")]
public class AdminLoginController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AdminLoginController> _logger;

    public AdminLoginController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<AdminLoginController> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string email, string password, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError(string.Empty, "Email and password are required.");
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Admin login successful for {email}");
                return RedirectToAction("Index", "AdminDashboard");
            }
        }

        _logger.LogWarning($"Invalid admin login attempt for {email}");
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View();
    }
}
