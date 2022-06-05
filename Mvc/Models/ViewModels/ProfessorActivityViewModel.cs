using Data.DTO;
using Data.DTO.Create;

namespace courseWork.Models.ViewModels;

public class ProfessorActivityViewModel
{
    public ICollection<ActivityDto> Activities { get; set; }

    public RateStudentsActivityModel RateStudentsActivityModel { get; set; }
}