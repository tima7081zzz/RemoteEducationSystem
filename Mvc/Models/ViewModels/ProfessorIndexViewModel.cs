using Data.DTO;
using Data.DTO.Create;

namespace courseWork.Models.ViewModels;

public class ProfessorIndexViewModel
{
    public string CreateGroupName { get; set; }
    public IEnumerable<UserDto> Students { get; set; }
    public IEnumerable<SubjectDto> Subjects { get; set; }
    public ICollection<GroupFullDto> Groups { get; set; }
    public AddStudentToGroupModel AddStudentToGroupModel { get; set; }
    public AddSubjectToGroupModel AddSubjectToGroupModel { get; set; }
}