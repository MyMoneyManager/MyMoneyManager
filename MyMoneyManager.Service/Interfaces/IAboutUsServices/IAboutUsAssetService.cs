using Microsoft.AspNetCore.Http;
using MyMoneyManager.Service.DTOs.AboutUsAssets;

namespace MyMoneyManager.Service.Interfaces.IAboutUsServices;

public interface IAboutUsAssetService
{
    Task<bool> RemoveAsync(long id);
    Task<AboutUsAssetForResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<AboutUsAssetForResultDto>> RetrieveAllAsync();
    Task<AboutUsAssetForResultDto> AddAsync(AboutUsAssetForCreationDto dto);
    Task<AboutUsAssetForResultDto> ModifyAsync(long id, AboutUsAssetForUpdateDto dto);
}
