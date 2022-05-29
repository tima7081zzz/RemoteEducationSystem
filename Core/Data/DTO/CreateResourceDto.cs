using Domain.Enums;

namespace Data.DTO;

public class CreateResourceDto
{
    public EResourceType Type { get; set; }
    public string Name { get; set; }
    public int? SubjectId { get; set; }
}