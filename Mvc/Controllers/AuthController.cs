using System.Security.Claims;
using courseWork.Models;
using Data.DTO;
using Domain.Enums;
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
            return RedirectToAction("Login");
        }

        await Authenticate(user);

        return user.Role switch
        {
            EUserRole.Admin => RedirectToAction("Index", "Admin"),
            EUserRole.Professor => RedirectToAction("Index", "Professor"),
            EUserRole.Student => RedirectToAction("Index", "Student"),
            _ => throw new ArgumentOutOfRangeException()
        };
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