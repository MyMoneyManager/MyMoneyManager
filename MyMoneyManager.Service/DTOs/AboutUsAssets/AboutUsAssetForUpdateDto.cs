using Microsoft.AspNetCore.Http;

namespace MyMoneyManager.Service.DTOs.AboutUsAssets;

public class AboutUsAssetForUpdateDto
{
    public long AboutUsId { get; set; }
    public IFormFile Image { get; set; }
}
