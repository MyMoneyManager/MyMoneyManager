using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Domain.Entities.AboutUs;

public class AboutUsAsset : Auditable
{
    public long AboutUsId { get; set; }
    public AboutUs AboutUs { get; set; }
    public string Image { get; set; }

}

