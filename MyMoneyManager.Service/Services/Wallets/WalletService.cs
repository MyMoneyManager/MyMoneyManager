using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Service.Commons.CollectionExtensions;
using MyMoneyManager.Service.DTOs.Wallets;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.Wallets;

namespace MyMoneyManager.Service.Services.Wallets;

public class WalletService : IWalletService
{
    private readonly IMapper mapper;
    private readonly IRepository<Wallet> repository;
    private readonly IRepository<User> userRepository;
    public WalletService(
        IRepository<Wallet> repository,
        IMapper mapper,
        IRepository<User> userRepository)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.userRepository = userRepository;

    }
    public async Task<WalletResultDto> AddAsync(WalletCreationDto dto)
    {
        var existUser = await userRepository.SelectByIdAsync(dto.UserId);

        if (existUser == null)
            throw new CustomException(404, "User is not found");

        var result = mapper.Map<Wallet>(dto);
        result.CreatedAt = DateTime.UtcNow;

        await repository.InsertAsync(result);

        return mapper.Map<WalletResultDto>(result);
    }

    public async Task<WalletResultDto> ModifyAsync(long id, WalletUpdateDto dto)
    {
        var existWallet = await repository.SelectByIdAsync(id);
        if (existWallet == null)
            throw new CustomException(404, "Wallet is not found");

        var existUser = await userRepository.SelectByIdAsync(dto.UserId);

        if (existUser == null)
            throw new CustomException(404, "User is not found");

        var result = mapper.Map<Wallet>(dto);
        result.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(result);

        return mapper.Map<WalletResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var wallet = await repository.SelectAll()
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (wallet == null)
            throw new CustomException(404, " Wallet is not found ");

        return await repository.DeleteAsync(id);
    }

    public async Task<List<WalletResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var wallets = await repository.SelectAll()
            .ToPagedList(@params)
            .AsNoTracking().
            ToListAsync();

        var result = mapper.Map<List<WalletResultDto>>(wallets);

        return result;
    }

    public async Task<WalletResultDto> RetrieveByIdAsync(long id)
    {
        var wallet = await repository.SelectAll()
            .Where(w => w.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (wallet == null)
            throw new CustomException(404, "Wallet is not found ");

        return mapper.Map<WalletResultDto>(wallet);
    }
}
