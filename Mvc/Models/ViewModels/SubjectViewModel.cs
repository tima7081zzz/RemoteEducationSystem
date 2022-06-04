using Data.DTO;

namespace courseWork.Models.ViewModels;

public class SubjectViewModel
{
    public string SubjectName { get; set; }
    public ICollection<SubjectDto> Subjects { get; set; }

    public ICollection<ProfessorIdNameModel> Professors { get; set; }

    public SetProfessorForSubjectRequestModel RequestModel { get; set; }
}