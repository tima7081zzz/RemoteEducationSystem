using Domain.Enums;

namespace Data.DTO.Create;

public class CreateActivityDto
{
    public int Id { get; set; }
    public EActivityType Type { get; set; }
    public string Name { get; set; }
    public int MaxGrade { get; set; }
    public int SubjectId { get; set; }
    public int ProfessorId { get; set; }
}