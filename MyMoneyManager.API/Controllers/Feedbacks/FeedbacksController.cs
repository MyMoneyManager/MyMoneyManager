using Microsoft.AspNetCore.Mvc;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.Feedbacks;
using MyMoneyManager.Service.DTOs.Users;
using MyMoneyManager.Service.Interfaces.IFeedbacks;

namespace MyMoneyManager.API.Controllers.Feedbacks;

public class FeedbacksController : BaseController
{
    private readonly IFeedbackService _feedbackService;

    public FeedbacksController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] FeedbackForCreationDto dto)
       => Ok(await _feedbackService.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    => Ok(await _feedbackService.RetrieveAllAsync(@params));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
    => Ok(await _feedbackService.DeleteAysnc(id));
}
