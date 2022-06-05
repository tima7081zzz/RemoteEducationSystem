using courseWork.Models;
using courseWork.Models.ViewModels;
using Data.DTO.Create;
using Domain.Enums;
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
            Groups = (await _professorService.GetAllGroupsByProfessorIdAsync(UserId!.Value, ct)).ToList(),
            Students = (await _professorService.GetAllStudentsAsync(ct)).ToList(),
            Subjects = (await _professorService.GetAllProfessorsSubjectsAsync(UserId!.Value, ct)).ToList(),
        };
        return View("Index", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddGroup(string createGroupName, CancellationToken ct)
    {
        await _professorService.CreateGroupAsync(createGroupName, UserId!.Value, ct);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddStudentToGroup(AddStudentToGroupModel addStudentToGroupModel, CancellationToken ct)
    {
        await _professorService.AddUserToGroupAsync(addStudentToGroupModel.StudentId, addStudentToGroupModel.GroupId, ct);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddSubjectToGroup(AddSubjectToGroupModel addSubjectToGroupModel, CancellationToken ct)
    {
        await _professorService.AddSubjectToGroupAsync(addSubjectToGroupModel.GroupId, addSubjectToGroupModel.SubjectId, UserId!.Value, ct);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddResourceToSubject(CreateResourceDto createResourceModel,
        CancellationToken ct)
    {
        await _professorService.AddResourceToSubjectAsync(new CreateResourceDto
        {
            Type = createResourceModel.Type,
            Name = createResourceModel.Name,
            SubjectId = createResourceModel.SubjectId,
        }, ct);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddActivityToSubject(CreateActivityDto createActivityModel,
        CancellationToken ct)
    {
        await _professorService.AddActivityToSubject(UserId!.Value, new CreateActivityDto
        {
            Type = createActivityModel.Type,
            Name = createActivityModel.Name,
            MaxGrade = createActivityModel.MaxGrade,
            SubjectId = createActivityModel.SubjectId,
            ProfessorId = UserId!.Value,
        }, ct);

        return RedirectToAction("Index");
    }
}