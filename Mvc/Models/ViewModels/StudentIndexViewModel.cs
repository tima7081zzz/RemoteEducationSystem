using Data.DTO;

namespace courseWork.Models.ViewModels;

public class StudentIndexViewModel
{
    public int ActivityId { get; set; }
    public ICollection<ActivityDto> Activities { get; set; }
}