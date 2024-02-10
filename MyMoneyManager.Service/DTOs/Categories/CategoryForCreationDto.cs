using MyMoneyManager.Domain.Enums;

namespace MyMoneyManager.Service.DTOs.Categories;

public class CategoryForCreationDto
{
    public CategoryType Name { get; set; }
}
