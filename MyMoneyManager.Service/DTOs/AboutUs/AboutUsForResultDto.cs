using MyMoneyManager.Service.DTOs.AboutUsAssets;

namespace MyMoneyManager.Service.DTOs.AboutUs;

public class AboutUsForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<AboutUsAssetForResultDto> AboutUsAssets { get; set; }
}
