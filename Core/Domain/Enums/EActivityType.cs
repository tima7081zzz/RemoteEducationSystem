using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum EActivityType : byte
{
    [Display(Name = "Unknown")]
    Unknown = 0,
    [Display(Name = "Lab")]
    Lab = 1,
    [Display(Name = "Report")]
    Report = 2,
    [Display(Name = "CourseProject")]
    CourseProject = 3,
    [Display(Name = "Test")]
    Test = 4,
}