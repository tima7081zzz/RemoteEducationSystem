using courseWork.Models.ViewModels;
using Data.DTO;
using Data.DTO.Create;
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

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var viewModel = new AdminIndexViewModel
        {
            Users = (await _adminService.GetAllUsersAsync(UserId!.Value, ct)).ToList(),
            Activities = (await _adminService.GetAllActivitiesAsync(UserId!.Value, ct)).ToList(),
            Resources = (await _adminService.GetAllResourcesAsync(UserId!.Value, ct)).ToList(),
        };

        return View("Index", viewModel);
    }

    public async Task<IActionResult> AddUser(CreateUserDto userModel, CancellationToken ct)
    {
        await _adminService.AddUserAsync(userModel, ct);
        return RedirectToAction("Index");
    }
}