using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities.Authorizations;
using MyMoneyManager.Service.Commons.CollectionExtensions;
using MyMoneyManager.Service.DTOs.RolePermisisons;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.IAuthorizations;

namespace MyMoneyManager.Service.Services.Authorizations;

public class RolePermissionService : IRolePermissionService
{
    private readonly IRepository<RolePermission> rolePermissionRepository;
    private readonly IMapper mapper;
    public RolePermissionService(IRepository<RolePermission> rolePermissionRepository, IMapper mapper)
    {
        this.rolePermissionRepository = rolePermissionRepository;
        this.mapper = mapper;
    }

    public async Task<RolePermissionForResultDto> CreateAsync(RolePermissionForCreateDto permission)
    {
        var rolePermission = await this.rolePermissionRepository.SelectAll()
            .Where(rp => rp.RoleId == permission.RoleId && rp.PermissonId == permission.PermissonId && rp.IsDeleted == true)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (rolePermission is not null)
            throw new CustomException(409, "RolePermission is already exist");

        var mappedRolePermission = this.mapper.Map<RolePermission>(permission);
        mappedRolePermission.CreatedAt = DateTime.UtcNow;
        var result = await this.rolePermissionRepository.InsertAsync(mappedRolePermission);

        return this.mapper.Map<RolePermissionForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var result = await this.rolePermissionRepository.SelectAll()
            .Where(rp => rp.Id == id && rp.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (result is null)
            throw new CustomException(404, "RolePermission is not available");

        await this.rolePermissionRepository.DeleteAsync(id);
        return true;
    }

    public async Task<List<RolePermissionForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var permissions = await rolePermissionRepository.SelectAll()
            .Where(p => p.IsDeleted == false && p.Permisson.IsDeleted == false && p.Role.IsDeleted == false)
            .ToPagedList(@params)
            .ToListAsync();

        return this.mapper.Map<List<RolePermissionForResultDto>>(permissions);
    }

    public async Task<RolePermissionForResultDto> ModifyAsync(RolePermissionForUpdateDto permission)
    {
        var rolePermission = await this.rolePermissionRepository.SelectAll()
            .Where(rp => rp.Id == permission.Id && rp.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (rolePermission is null)
            throw new CustomException(404, "RolePermission is not available");
        var result = this.mapper.Map(permission, rolePermission);
        result.UpdatedAt = DateTime.UtcNow;

        return this.mapper.Map<RolePermissionForResultDto>(result);
    }

    public async Task<RolePermissionForResultDto> RetrieveByIdAsync(long id)
    {
        var rolePermission = await this.rolePermissionRepository.SelectAll()
            .Where(rp => rp.Id == id && rp.IsDeleted == false && rp.Permisson.IsDeleted == false && rp.Role.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (rolePermission is null)
            throw new CustomException(404, "RolePermission is not found");

        return this.mapper.Map<RolePermissionForResultDto>(rolePermission);
    }

    public async Task<bool> CheckPermission(string role, string accessedMethod)
    {
        var permissions = await this.rolePermissionRepository.SelectAll()
            .Where(p => p.Role.Name.ToLower() == role.ToLower() && p.Permisson.IsDeleted == false && p.Role.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync();
        foreach (var permission in permissions)
        {
            if (permission?.Permisson?.Name.ToLower() == accessedMethod.ToLower())
                return true;
        }

        return false;

    }
}
