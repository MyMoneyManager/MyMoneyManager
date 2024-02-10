using MyMoneyManager.Domain.Enums;

namespace MyMoneyManager.Service.DTOs.Reports;

public class ReportForUpdateDto
{
    public long UserId { get; set; }
    public TransactionType TransactionType { get; set; }
}
