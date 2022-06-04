using Domain.Enums;

namespace Data.DTO;

public class ResourceDto
{
    public int Id { get; set; }
    public EResourceType Type { get; set; }
    public string Name { get; set; }
    public int? SubjectId { get; set; }
    public string SubjectName { get; set; }
}