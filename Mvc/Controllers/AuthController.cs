using System.Security.Claims;
using courseWork.Models;
using Data.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace courseWork.Controllers;
public class AuthController : Controller
{
    private readonly IAdminService _adminService;

    public AuthController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IActionResult Index()
    {
        return View("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View("Login");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUserDto loginUserModel, CancellationToken ct)
    {
        var user = await _adminService.GetUserAsync(loginUserModel,
            ct);
        if (user == null)
        {
            return RedirectToAction("Index");
        }

        await Authenticate(user);
        return View("Error");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View("Register");
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateUserDto registerModel, CancellationToken ct)
    {
        var user = await _adminService.GetUserAsync(new LoginUserDto
            {
                Email = registerModel.Email,
                Password = registerModel.Password
            },
            ct);

        if (user != null)
        {
            await Authenticate(user);
            //redirect
        }

        var newUser = await _adminService.AddUserAsync(registerModel, ct);
        await Authenticate(newUser);

        return View("Error");
    }

    private async Task Authenticate(UserDto user)
    {
        var userRole = UserRoles.ResolveUserRoleByEnumValue(user.Role);

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultRoleClaimType, userRole),
            new(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }
}