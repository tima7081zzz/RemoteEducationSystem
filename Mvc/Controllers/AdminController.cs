using courseWork.Models;
using courseWork.Models.ViewModels;
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

    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> AddUser(CreateUserDto userModel, CancellationToken ct)
    {
        await _adminService.AddUserAsync(userModel, ct);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Subject(CancellationToken ct)
    {
        var viewModel = new SubjectViewModel
        {
            Subjects = (await _adminService.GetAllSubjectsAsync(UserId!.Value,
                ct)).ToList(),
            Professors = (await _adminService.GetAllProfessorsAsync(ct))
                .Select(x => new ProfessorIdNameModel {FullName = x.Fullname, Id = x.Id}).ToList(),
        };

        return View("Subject", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubject(int userId, string subjectName, CancellationToken ct)
    {
        var subjectId = await _adminService.CreateSubjectAsync(subjectName, ct);

        return RedirectToAction("Subject");
    }

    [HttpPost]
    public async Task<IActionResult> SetProfessorForSubject(SetProfessorForSubjectRequestModel requestModel, CancellationToken ct)
    {
        await _adminService.SetProfessorForSubjectAsync(requestModel.ProfessorId, requestModel.SubjectId, ct);

        return RedirectToAction("Subject");
    }
}