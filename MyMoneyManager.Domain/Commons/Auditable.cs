namespace MyMoneyManager.Domain.Commons;

public abstract class Auditable
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public long? UpdatedBy { get; set; }
    public long? DeletedBy { get; set; }
}
