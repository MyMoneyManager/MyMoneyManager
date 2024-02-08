using MyMoneyManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyMoneyManager.Service.DTOs.Users;

public class UserForUpdateDto
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    //[EmailAddress(ErrorMessage = "Please enter valid email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    public UserGenderType GenderType { get; set; }
}
