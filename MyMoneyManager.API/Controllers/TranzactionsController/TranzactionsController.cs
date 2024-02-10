using Microsoft.AspNetCore.Mvc;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.Transactions;
using MyMoneyManager.Service.DTOs.Users;
using MyMoneyManager.Service.Interfaces.ITransactionServices;

namespace MyMoneyManager.API.Controllers.TranzactionsController;

public class TranzactionsController : BaseController
{
    private readonly ITranzactionService _tranzactionService;

    public TranzactionsController(ITranzactionService tranzactionService)
    {
        _tranzactionService = tranzactionService;
    }

    /// <summary>
    /// Handles HTTP POST requests to insert a new user into the database.
    /// </summary>
    /// <param name="dto">Data for creating the new user.</param>
    /// <returns>Returns an IActionResult with the result of the insertion operation.</returns>
    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] TranzactionForCreationDto dto)
        => Ok(await _tranzactionService.AddAsync(dto));


    /// <summary>
    /// Handles HTTP GET requests to retrieve all users with optional pagination parameters.
    /// </summary>
    /// <param name="@params">Optional pagination parameters for controlling the result set.</param>
    /// <returns>Returns an IActionResult with the result of the retrieval operation.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await _tranzactionService.RetrieveAllAsync(@params));


    /// <summary>
    /// Handles HTTP GET requests to retrieve a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be retrieved.</param>
    /// <returns>Returns an IActionResult with the result of the retrieval operation.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        => Ok(await _tranzactionService.RetrieveByIdAsync(id));


    /// <summary>
    /// Handles HTTP DELETE requests to remove a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be removed.</param>
    /// <returns>Returns an IActionResult with the result of the removal operation.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        => Ok(await _tranzactionService.RemoveAsync(id));


    /// <summary>
    /// Handles HTTP PUT requests to update an existing user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be updated.</param>
    /// <param name="dto">Data for updating the existing user.</param>
    /// <returns>Returns an IActionResult with the result of the update operation.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] TranzactionForUpdateDto dto)
        => Ok(await _tranzactionService.ModifyAsync(id, dto));
}
