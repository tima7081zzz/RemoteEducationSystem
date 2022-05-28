using Domain.Enums;

namespace Data.DTO;

public class ActivityDto
{
    public int Id { get; set; }
    public EActivityType Type { get; set; }
    public string Name { get; set; }
    public int MaxGrade { get; set; }
    public int SubjectId { get; set; }
}