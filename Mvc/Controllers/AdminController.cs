using Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace courseWork.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : BaseController
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IActionResult Index()
    {
        //modify view
        return View("Error");
    }

    public async Task<IActionResult> AddUser(CreateUserDto userModel, CancellationToken ct)
    {
        await _adminService.AddUserAsync(userModel, ct);
        //modify view
        return View("Error");
    }
}