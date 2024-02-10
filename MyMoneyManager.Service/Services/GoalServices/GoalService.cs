using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Service.Commons.CollectionExtensions;
using MyMoneyManager.Service.DTOs.Goals;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.IGoalService;

namespace MyMoneyManager.Service.Services.GoalServices;

public class GoalService : IGoalService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Goal> _repository;
    private readonly IRepository<User> _userRepository;

    public GoalService(IMapper mapper, IRepository<Goal> repository, IRepository<User> userRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<GoalForResultDto> AddAsync(GoalForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CustomException(404, "User not found");

        var goal = await _repository.SelectAll()
            .Where(g => g.Name.ToLower() == dto.Name.ToLower())
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (goal is not null)
            throw new CustomException(409, "Goal is already exists");

        var mapped = _mapper.Map<Goal>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await _repository.InsertAsync(mapped);

        return _mapper.Map<GoalForResultDto>(mapped);
    }

    public async Task<GoalForResultDto> ModifyAsync(long id, GoalForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CustomException(404, "User is not found");

        var goal = await _repository.SelectAll()
            .Where(g => g.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (goal is null)
            throw new CustomException(409, "Goal is not found");

        var mapped = _mapper.Map(dto, goal);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = await _repository.UpdateAsync(mapped);

        return _mapper.Map<GoalForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long Id)
    {
        var goal = await _repository.SelectAll()
            .Where(g => g.Id == Id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (goal is null)
            throw new CustomException(404, "Goal is not found");

        return await _repository.DeleteAsync(Id);
    }

    public async Task<IEnumerable<GoalForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var goal = await _repository.SelectAll()
            .Include(g => g.User) 
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<GoalForResultDto>>(goal);
    }

    public async Task<GoalForResultDto> RetrieveByIdAsync(long Id)
    {
        var goal = await _repository.SelectAll()
            .Where(g => g.Id == Id)
            .Include(g => g.User)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (goal is null)
            throw new CustomException(404, "Goal is not found");

        return _mapper.Map<GoalForResultDto>(goal);
    }
}
