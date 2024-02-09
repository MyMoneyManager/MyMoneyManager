using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Domain.Entities.AboutUs;

public class AboutUs : Auditable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<AboutUsAsset> AboutUsAssets { get; set; }
}
