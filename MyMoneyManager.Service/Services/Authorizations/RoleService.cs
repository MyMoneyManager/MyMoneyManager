using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities.Authorizations;
using MyMoneyManager.Service.Commons.CollectionExtensions;
using MyMoneyManager.Service.DTOs.Roles;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.IAuthorizations;
using MyMoneyManager.Shared.Helpers;

namespace MyMoneyManager.Service.Services.Authorizations;

public class RoleService : IRoleService
{
    private readonly IRepository<Role> roleRepository;
    private readonly IMapper mapper;

    public RoleService(IRepository<Role> roleRepository, IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task<RoleForResultDto> AddAsync(RoleForCreationDto dto)
    {
        var exist = await this.roleRepository.SelectAll()
            .Where(r => r.Name == dto.Name)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (exist is not null)
            throw new CustomException(404, "Role is already exist");

        var mappedDto = mapper.Map<Role>(dto);
        mappedDto.UpdatedAt = DateTime.UtcNow;
        await this.roleRepository.InsertAsync(mappedDto);

        return mapper.Map<RoleForResultDto>(mappedDto);
    }

    public async Task<IEnumerable<RoleForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var roles = await roleRepository.SelectAll()
        .ToPagedList(@params)
        .AsNoTracking()
        .ToListAsync();

        return mapper.Map<IEnumerable<RoleForResultDto>>(roles);
    }

    public async Task<bool> ModifyAsync(RoleForUpdateDto dto)
    {
        var exist = await this.roleRepository.SelectAll()
            .Where(r => r.Name == dto.Name && r.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (exist is null)
            throw new CustomException(404, "Role is not found");

        var mappedDto = mapper.Map(dto, exist);
        mappedDto.UpdatedAt = DateTime.UtcNow;
        mappedDto.UpdatedBy = HttpContextHelper.UserId;

        return true;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var exist = await this.roleRepository.SelectAll()
            .Where(r => r.Id == id && r.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync ();
        if (exist is null)
            throw new CustomException(404, "Role is not found");

        await this.roleRepository.DeleteAsync(id);

        return true;
    }

    public async Task<Role> RetrieveByIdForAuthAsync(long id)
    {
        var exist = await this.roleRepository.SelectAll()
        .Where(u => u.Id == id && u.IsDeleted == false)
        .AsNoTracking()
        .FirstOrDefaultAsync();

        if (exist is null)
            throw new CustomException(404, "Role is not found");

        return mapper.Map<Role>(exist);
    }
    public async Task<RoleForResultDto> RetrieveByIdAsync(long id)
    {
        var role = await this.roleRepository.SelectAll()
            .Where(u => u.Id == id && u.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return this.mapper.Map<RoleForResultDto>(role);
    }
}