using Data.DTO;
using Data.DTO.Create;

namespace courseWork.Models.ViewModels;

public class AdminIndexViewModel
{
    public ICollection<UserDto> Users { get; set; }
    public ICollection<CreateActivityDto> Activities { get; set; }
    public ICollection<ResourceDto> Resources { get; set; }
    public CreateUserDto UserModel { get; set; }
}