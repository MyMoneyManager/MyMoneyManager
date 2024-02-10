using Microsoft.AspNetCore.Mvc;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.Wallets;
using MyMoneyManager.Service.Interfaces.Wallets;

namespace MyMoneyManager.API.Controllers.Wallets;

public class WalletsController : BaseController
{
    private readonly IWalletService walletService;
    public WalletsController(IWalletService walletService)
    {
        this.walletService = walletService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(WalletCreationDto dto)
        => Ok(await this.walletService.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(PaginationParams @params)
        => Ok(await walletService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await walletService.RetrieveByIdAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await walletService.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(long id, WalletUpdateDto dto)
        => Ok(await walletService.ModifyAsync(id, dto));
}
