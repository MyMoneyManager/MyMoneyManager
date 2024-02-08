using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Service.Commons.CollectionExtensions;
using MyMoneyManager.Service.DTOs.Users;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.Users;
using System.Data.Common;

namespace MyMoneyManager.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Adds new User to database
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var user = userRepository.SelectAll()
            .AsNoTracking()
            .Where(u => u.Email == dto.Email)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CustomException(409, "USer already exists");

        var mapped = mapper.Map<User>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await userRepository.InsertAsync(mapped);

        return mapper.Map<UserForResultDto>(mapped);
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var user = await userRepository.SelectAll()
           .AsNoTracking()
           .Where(u => u.Id == id)
           .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "User not found");

        var mapped = mapper.Map(dto, user);
        mapped.UpdatedAt = DateTime.UtcNow;
        await userRepository.UpdateAsync(mapped);

        return mapper.Map<UserForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await userRepository.SelectAll()
            .AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "User not found");

        await userRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var users = await userRepository.SelectAll()
            .ToPagedList(@params)
             .AsNoTracking()
             .ToListAsync();
        if (users is null)
            throw new CustomException(404, "User is not found!");

        return mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var user = await userRepository.SelectAll()
               .Where(u => u.Id == id)
               .AsNoTracking()
               .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "User is not found!");

        return mapper.Map<UserForResultDto>(user);
    }
}
