using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Service.Commons.CollectionExtensions;
using MyMoneyManager.Service.DTOs.Reports;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.IReportService;

namespace MyMoneyManager.Service.Services.ReportServices;

public class ReportService : IReportServie
{
    private readonly IMapper _mapper;
    private readonly IRepository<Report> _repository;
    private readonly IRepository<User> _userRepository;

    public ReportService(IMapper mapper, IRepository<Report> repository, IRepository<User> userRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<ReportForResultDto> AddAsync(ReportForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CustomException(404,"User is not found");

        var report = await _repository.SelectAll()
            .Where(r => r.TransactionType == dto.TransactionType)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (report is not null)
            throw new CustomException(409, "Report is already exists");

        var mapped = _mapper.Map(dto, report);
        mapped.CreatedAt = DateTime.UtcNow;
        var result = await _repository.InsertAsync(mapped);

        return _mapper.Map<ReportForResultDto>(result);
    }

    public async Task<ReportForResultDto> ModifyAsync(long id, ReportForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CustomException(404, "User is not found");

        var report = await _repository.SelectAll()
            .Where(r => r.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (report is  null)
            throw new CustomException(404, "Report is not found");

        var mapped = _mapper.Map(dto, report);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = await _repository.UpdateAsync(mapped);

        return _mapper.Map<ReportForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var report = await _repository.SelectAll()
            .Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (report is null)
            throw new CustomException(404,"Report is not found");

        return await _repository.DeleteAsync(id);
    }


    public async Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var report = await _repository.SelectAll()
            .Include(r => r.User)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<ReportForResultDto>>(report);
    }

    public async Task<ReportForResultDto> RetrieveByIdAsync(long id)
    {
        var report = await _repository.SelectAll()
            .Where(u => u.Id == id)
            .Include(r => r.User)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (report is null)
            throw new CustomException(404, "Report is not found");

        return _mapper.Map<ReportForResultDto>(report);
    }
}
