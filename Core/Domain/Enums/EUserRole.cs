using System.ComponentModel;

namespace Domain.Enums;

public enum EUserRole : byte
{
    [Description("Admin")]
    Admin = 0,

    [Description("Professor")]
    Professor = 1,

    [Description("Student")]
    Student = 2,
}