using MyMoneyManager.Domain.Enums;
namespace MyMoneyManager.Service.DTOs.Reports;
public class ReportForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public TransactionType TransactionType { get; set; }
}
