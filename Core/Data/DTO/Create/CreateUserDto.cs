using Domain.Enums;

namespace Data.DTO.Create;

public class CreateUserDto
{
    public EUserRole Role { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}