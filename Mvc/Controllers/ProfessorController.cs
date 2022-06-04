using courseWork.Models;
using courseWork.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace courseWork.Controllers;

[Authorize(Roles = "Professor")]
public class ProfessorController : BaseController
{
    private readonly IProfessorService _professorService;

    public ProfessorController(IProfessorService professorService)
    {
        _professorService = professorService;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var viewModel = new ProfessorIndexViewModel
        {
            Groups = (await _professorService.GetAllGroupsByProfessorIdAsync(UserId!.Value, ct)).ToList()
        };
        return View("Index", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddGroup(CreateGroupDto createGroupModel, CancellationToken ct)
    {
        await _professorService.CreateGroupAsync(createGroupModel.groupName, UserId!.Value, ct);

        return RedirectToAction("Index");
    }
}