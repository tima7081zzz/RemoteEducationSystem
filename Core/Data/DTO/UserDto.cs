using Domain.Enums;

namespace Data.DTO;

public class UserDto
{
    public int Id { get; set; }
    public EUserRole Role { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
}