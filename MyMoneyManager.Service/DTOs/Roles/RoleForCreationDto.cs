using System.ComponentModel.DataAnnotations;

namespace MyMoneyManager.Service.DTOs.Roles;

public class RoleForCreationDto
{
    [Required]
    public string Name { get; set; }
}
