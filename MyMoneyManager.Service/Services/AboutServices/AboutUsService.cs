using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Entities.AboutUs;
using MyMoneyManager.Service.DTOs.AboutUs;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.IAboutUsServices;

namespace MyMoneyManager.Service.Services.AboutServices;

public class AboutUsService : IAboutUsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<AboutUs> _repository;

    public AboutUsService(IMapper mapper, IRepository<AboutUs> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    /// <summary>
    /// Adds a new About Us entry to the database.
    /// </summary>
    /// <param name="dto">The data transfer object containing information for creating the new About Us entry.</param>
    /// <returns>
    /// A task representing the added About Us information in the form of AboutUsForResultDto.
    /// </returns>
    /// <exception cref="CustomException">Thrown if an About Us entry with the specified title already exists and is not marked as deleted (HTTP 404 Not Found).</exception>
    public async Task<AboutUsForResultDto> AddAsync(AboutUsForCreationDto dto)
    {
        var aboutUs = await _repository.SelectAll()
            .Where(au => au.Title == dto.Title && au.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (aboutUs is not null)
            throw new CustomException(404, "AboutUS alrerady exists");

        var mapped = _mapper.Map<AboutUs>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        var result = await _repository.InsertAsync(mapped);

        return _mapper.Map<AboutUsForResultDto>(result);
    }

    /// <summary>
    /// Retrieves an About Us entry from the database based on the provided About Us ID,
    /// including associated About Us assets.
    /// </summary>
    /// <param name="id">The unique identifier of the About Us entry to be retrieved.</param>
    /// <returns>
    /// A task representing the retrieved About Us information in the form of AboutUsForResultDto.
    /// </returns>
    /// <exception cref="CustomException">Thrown if the About Us entry with the specified ID is not found (HTTP 409 Conflict).</exception>
    public async Task<AboutUsForResultDto> GetByIdAsync(long id)
    {
        var aboutUs = await _repository.SelectAll()
            .Where(a => a.IsDeleted == false && a.Id == id)
            .Include(a => a.AboutUsAssets)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (aboutUs is null)
            throw new CustomException(409, "AboutUs is not found");

        return _mapper.Map<AboutUsForResultDto>(aboutUs);
    }

    /// <summary>
    /// Modifies an existing About Us entry in the database based on the provided About Us ID and update information.
    /// </summary>
    /// <param name="id">The unique identifier of the About Us entry to be modified.</param>
    /// <param name="dto">The data transfer object containing the updated information for the About Us entry.</param>
    /// <returns>
    /// A task representing the modified About Us information in the form of AboutUsForResultDto.
    /// </returns>
    /// <exception cref="CustomException">Thrown if the About Us entry with the specified ID is not found (HTTP 409 Conflict).</exception>
    public async Task<AboutUsForResultDto> ModifyAsync(long id, AboutUsForUpdateDto dto)
    {
        var aboutUs = await _repository.SelectAll()
            .Where(a => a.IsDeleted == false && a.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (aboutUs is null)
            throw new CustomException(409, "AboutUs is not found");
        var mapped = _mapper.Map(dto, aboutUs);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = await _repository.InsertAsync(mapped);

        return _mapper.Map<AboutUsForResultDto>(result);
    }

    /// <summary>
    /// Removes an existing About Us entry from the database based on the provided About Us ID.
    /// Additionally, marks associated About Us assets as deleted without physically removing them.
    /// </summary>
    /// <param name="id">The unique identifier of the About Us entry to be removed.</param>
    /// <returns>
    /// A task representing the success of the removal operation (true if successful).
    /// </returns>
    /// <exception cref="CustomException">Thrown if the About Us entry with the specified ID is not found (HTTP 409 Conflict).</exception>
    public async Task<bool> RemoveAsync(long id)
    {
        var aboutUs = await _repository.SelectAll()
            .Where(a => a.IsDeleted == false && a.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (aboutUs is not null)
            throw new CustomException(409, "AboutUs is not found");

        foreach (var relatedEntity in aboutUs.AboutUsAssets)
        {
            if (relatedEntity.Image != null)
            {
                relatedEntity.IsDeleted = true;
            }
        }

        return await _repository.DeleteAsync(id);
    }

    /// <summary>
    /// Retrieves all non-deleted About Us entries from the database, including associated About Us assets.
    /// </summary>
    /// <returns>
    /// A task representing the collection of non-deleted About Us information in the form of AboutUsForResultDto.
    /// </returns>
    public async Task<IEnumerable<AboutUsForResultDto>> RetrieveAllAsync()
    {
        var aboutUsList = await _repository.SelectAll()
            .Where(e => e.IsDeleted == false)
            .Include(e => e.AboutUsAssets)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AboutUsForResultDto>>(aboutUsList);
    }
}
