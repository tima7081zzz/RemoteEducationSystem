using courseWork.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace courseWork.Controllers;

[Authorize(Roles = "Student")]
public class StudentController : BaseController
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var viewModel = new StudentIndexViewModel
        {
            Activities = (await _studentService.GetStudentsActivitiesAsync(UserId!.Value, ct)).ToList(),
        };

        return View("Index", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DoActivity(int activityId, CancellationToken ct)
    {
        await _studentService.DoActivityAsync(activityId, UserId!.Value, ct);

        return RedirectToAction("Index");
    }

}