namespace Data.DTO;

public class GroupFullDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<string> StudentsNames { get; set; }
    public ICollection<string> SubjectNames { get; set; }
}