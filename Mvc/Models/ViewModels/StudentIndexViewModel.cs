using Data.DTO;

namespace courseWork.Models.ViewModels;

public class StudentIndexViewModel
{
    public ICollection<ActivityDto> Activities { get; set; }
}