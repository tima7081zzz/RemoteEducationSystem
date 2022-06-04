using Data.DTO;
using Data.DTO.Create;

namespace courseWork.Models.ViewModels;

public class ProfessorIndexViewModel
{
    public CreateGroupDto CreateGroupModel { get; set; }
    public ICollection<GroupDto> Groups { get; set; }
}