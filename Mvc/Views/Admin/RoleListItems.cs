using courseWork.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace courseWork.Views.Admin;

public class RoleListItems
{
    public static List<SelectListItem> Items = new()
    {
        new SelectListItem(UserRoles.Admin, EUserRole.Admin.ToString()),
        new SelectListItem(UserRoles.Professor, EUserRole.Professor.ToString()),
        new SelectListItem(UserRoles.Student, EUserRole.Student.ToString()),
    };
}