using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Service.Commons.CollectionExtensions;
using MyMoneyManager.Service.DTOs.Categories;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.ICategoryServices;

namespace MyMoneyManager.Service.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Category> _repository;

    public CategoryService(IMapper mapper, IRepository<Category> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CategoryForResultDto> AddAsync(CategoryForCreationDto dto)
    {
        var category = await _repository.SelectAll()
            .Where(c => c.Name == dto.Name)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (category is not null)
            throw new CustomException(409,"Category is already exists");
        var mapped = _mapper.Map<Category>(dto);
        mapped.CreatedAt = DateTime.UtcNow;

        var result = await _repository.InsertAsync(mapped);

        return _mapper.Map<CategoryForResultDto>(result);
    }

    public async Task<CategoryForResultDto> ModifyAsync(long id, CategoryForUpdateDto dto)
    {
        var category = await _repository.SelectAll()
            .Where(c => c.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (category is null)
            throw new CustomException(409, "Category is not found");
        var mapped = _mapper.Map(dto, category);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = _repository.UpdateAsync(mapped);

        return _mapper.Map<CategoryForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var category = await _repository.SelectAll()
            .Where(c => c.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (category is null)
            throw new CustomException(409, "Category is not found");

        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CategoryForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var category = await _repository.SelectAll()
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<CategoryForResultDto>>(category);
    }

    public async Task<CategoryForResultDto> RetrieveByIdAsync(long id)
    {
        var category = await _repository.SelectAll()
            .Where(c => c.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (category is null)
            throw new CustomException(409, "Category is not found");

        return _mapper.Map<CategoryForResultDto>(category);
    }
}
