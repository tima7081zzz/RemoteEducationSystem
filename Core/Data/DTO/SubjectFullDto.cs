namespace Data.DTO;

public class SubjectFullDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<string> ProfessorsNames { get; set; }
}