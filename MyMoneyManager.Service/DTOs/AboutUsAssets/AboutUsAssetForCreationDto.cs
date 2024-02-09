using Microsoft.AspNetCore.Http;

namespace MyMoneyManager.Service.DTOs.AboutUsAssets;

public class AboutUsAssetForCreationDto
{
    public long AboutUsId { get; set; }
    public IFormFile Image {  get; set; }
}
