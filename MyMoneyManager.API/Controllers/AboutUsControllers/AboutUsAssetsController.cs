using Microsoft.AspNetCore.Mvc;
using MyMoneyManager.Service.DTOs.AboutUsAssets;
using MyMoneyManager.Service.Interfaces.IAboutUsServices;

namespace MyMoneyManager.API.Controllers.AboutUsControllers;

public class AboutUsAssetsController : BaseController
{
    private readonly IAboutUsAssetService _aboutUsAssetService;

    public AboutUsAssetsController(IAboutUsAssetService aboutUsAssetService)
    {
        _aboutUsAssetService = aboutUsAssetService;
    }

    /// <summary>
    /// Handles HTTP POST requests to insert a new user into the database.
    /// </summary>
    /// <param name="dto">Data for creating the new user.</param>
    /// <returns>Returns an IActionResult with the result of the insertion operation.</returns>
    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromForm] AboutUsAssetForCreationDto dto)
        => Ok(await _aboutUsAssetService.AddAsync(dto));

    /// <summary>
    /// Handles HTTP GET requests to retrieve all users with optional pagination parameters.
    /// </summary>
    /// <returns>Returns an IActionResult with the result of the retrieval operation.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await _aboutUsAssetService.RetrieveAllAsync());

    /// <summary>
    /// Handles HTTP GET requests to retrieve a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be retrieved.</param>
    /// <returns>Returns an IActionResult with the result of the retrieval operation.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        => Ok(await _aboutUsAssetService.RetrieveByIdAsync(id));


    /// <summary>
    /// Handles HTTP DELETE requests to remove a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be removed.</param>
    /// <returns>Returns an IActionResult with the result of the removal operation.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        => Ok(await _aboutUsAssetService.RemoveAsync(id));


    /// <summary>
    /// Handles HTTP PUT requests to update an existing user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be updated.</param>
    /// <param name="dto">Data for updating the existing user.</param>
    /// <returns>Returns an IActionResult with the result of the update operation.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] AboutUsAssetForUpdateDto dto)
        => Ok(await _aboutUsAssetService.ModifyAsync(id, dto));
}
