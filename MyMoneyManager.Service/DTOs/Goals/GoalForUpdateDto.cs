﻿namespace MyMoneyManager.Service.DTOs.Goals;

public class GoalForUpdateDto
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public decimal TargetAmount { get; set; }
    public DateTime TargetDate { get; set; }
}
