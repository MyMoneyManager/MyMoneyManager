using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Service.Commons.CollectionExtensions;
using MyMoneyManager.Service.DTOs.Transactions;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.ITransactionServices;

namespace MyMoneyManager.Service.Services.TransactionServices;

public class TranzactionService : ITranzactionService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Wallet> _walletRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Tranzaction> _tranzactionRepository;

    public TranzactionService(IMapper mapper, 
        IRepository<Wallet> walletRepository, 
        IRepository<Category> categoryRepository,
        IRepository<Tranzaction> tranzactionRepository)
    {
        _mapper = mapper;
        _walletRepository = walletRepository;
        _categoryRepository = categoryRepository;
        _tranzactionRepository = tranzactionRepository;
    }

    public async Task<TranzactionForResultDto> AddAsync(TranzactionForCreationDto dto)
    {
        var wallet = await _walletRepository.SelectAll()
            .Where(w => w.Id == dto.WalletId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (wallet is null)
            throw new CustomException(404, "Wallet is not found");
        var category = await _categoryRepository.SelectAll()
            .Where(c => c.Id == dto.CategoryId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (category is null)
            throw new CustomException(404, "Category is not found");
        var transaction = await _tranzactionRepository.SelectAll()
            .Where(t => t.Balance == dto.Balance)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (transaction is not null)
            throw new CustomException(409, "Transaction is already exists");

        var mapped = _mapper.Map<Tranzaction>(dto);
        mapped.CreatedAt = DateTime.UtcNow;

        var result = await _tranzactionRepository.InsertAsync(mapped);

        return _mapper.Map<TranzactionForResultDto>(result);
    }
    public async Task<TranzactionForResultDto> ModifyAsync(long id, TranzactionForUpdateDto dto)
    {
        var wallet = await _walletRepository.SelectAll()
            .Where(w => w.Id == dto.WalletId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (wallet is null)
            throw new CustomException(404, "Wallet is not found");
        var category = await _categoryRepository.SelectAll()
            .Where(c => c.Id == dto.CategoryId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (category is null)
            throw new CustomException(404, "Category is not found");
        var transaction = await _tranzactionRepository.SelectAll()
            .Where(t => t.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (transaction is  null)
            throw new CustomException(404, "Transaction is not found");
        var mapped = _mapper.Map(dto, transaction);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = await _tranzactionRepository.UpdateAsync(mapped);

        return _mapper.Map<TranzactionForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var transaction = await _tranzactionRepository.SelectAll()
            .Where(t => t.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (transaction is null)
            throw new CustomException(404, "Transaction is not found");

        return await _tranzactionRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<TranzactionForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var transaction = await _tranzactionRepository.SelectAll()
            .Include(t => t.Wallet)
            .Include(t => t.Category)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<TranzactionForResultDto>>(transaction);
    }

    public async Task<TranzactionForResultDto> RetrieveByIdAsync(long id)
    {
        var transaction = await _tranzactionRepository.SelectAll()
            .Where(t => t.Id == id)
            .Include(t => t.Wallet)
            .Include(t => t.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (transaction is null)
            throw new CustomException(404, "Transaction is not found");

        return _mapper.Map<TranzactionForResultDto>(transaction);
    }
}
