using MyMoneyManager.Service.DTOs.AboutUs;
using MyMoneyManager.Service.DTOs.AboutUsAssets;

namespace MyMoneyManager.Service.Interfaces.IAboutUsServices;

public interface IAboutUsService
{
    Task<bool> RemoveAsync (long id);
    Task<AboutUsForResultDto> GetByIdAsync (long id);
    Task<AboutUsForResultDto> AddAsync (AboutUsForCreationDto dto);
    Task<IEnumerable<AboutUsForResultDto>> RetrieveAllAsync ();
    Task<AboutUsForResultDto> ModifyAsync (long id,AboutUsForUpdateDto dto);
}
