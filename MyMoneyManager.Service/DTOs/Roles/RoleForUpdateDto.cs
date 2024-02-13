using System.ComponentModel.DataAnnotations;

namespace MyMoneyManager.Service.DTOs.Roles;

public class RoleForUpdateDto
{
    [Required]
    public long RoleId { get; set; }
    [Required]
    public string Name { get; set; }
}
