using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace courseWork.Controllers;

public class BaseController : Controller
{
    internal int? UserId => User.Identity!.IsAuthenticated ? (int)(Convert.ChangeType(User.FindFirst(ClaimTypes.NameIdentifier), typeof(int)) ?? 0) : null;
}