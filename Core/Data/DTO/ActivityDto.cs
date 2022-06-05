using Domain.Enums;

namespace Data.DTO;

public class ActivityDto
{
    public int Id { get; set; }
    public EActivityType Type { get; set; }
    public string Name { get; set; }
    public int MaxGrade { get; set; }
    public int? Grade { get; set; }
    public int SubjectId { get; set; }
    public bool IsDone { get; set; }
    public string SubjectName { get; set; }
    public string StudentName { get; set; }
    public int StudentId { get; set; }
}