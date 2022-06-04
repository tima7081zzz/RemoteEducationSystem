using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace courseWork.Controllers;

public class StudentController : BaseController
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View("Index");
    }

    [HttpPut]
    public async Task<IActionResult> DoActivity(int activityId, CancellationToken ct)
    {
        await _studentService.DoActivityAsync(activityId, UserId!.Value, ct);

        return RedirectToAction("Index");
    }

}