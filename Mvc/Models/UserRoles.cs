using Domain.Enums;

namespace courseWork.Models;

public static class UserRoles
{
    public static string Admin = "Admin";
    public static string Professor = "Professor";
    public static string Student = "Student";

    public static string ResolveUserRoleByEnumValue(EUserRole userRole)
    {
        return userRole switch
        {
            EUserRole.Admin => Admin,
            EUserRole.Professor => Professor,
            EUserRole.Student => Student,
            _ => throw new ArgumentOutOfRangeException(nameof(userRole), userRole, null)
        };
    }
}