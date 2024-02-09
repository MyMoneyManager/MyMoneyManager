using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Data.Repositories;
using MyMoneyManager.Domain.Entities.AboutUs;
using MyMoneyManager.Service.DTOs.AboutUsAssets;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.IAboutUsServices;
using MyMoneyManager.Shared.Helpers;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace MyMoneyManager.Service.Services.AboutServices;

public class AboutUsAssetService : IAboutUsAssetService
{
    private readonly IMapper _mapper;
    private readonly IRepository<AboutUsAsset> _aboutUsAssetRepository;
    private readonly IRepository<AboutUs> _aboutUsRepository;

    public AboutUsAssetService(IMapper mapper, IRepository<AboutUsAsset> aboutUsAssetRepository, IRepository<AboutUs> aboutUsRepository)
    {
        _mapper = mapper;
        _aboutUsAssetRepository = aboutUsAssetRepository;
        _aboutUsRepository = aboutUsRepository;
    }

    /// <summary>
    /// Adds a new asset for an About Us section to the database.
    /// </summary>
    /// <param name="dto">The data transfer object containing information for creating the new asset.</param>
    /// <returns>
    /// A task representing the added AboutUsAsset information in the form of AboutUsAssetForResultDto.
    /// </returns>
    /// <exception cref="CustomException">Thrown if the provided AboutUsId is not found or marked as deleted,
    /// if the image is null, or if the image format is not valid (HTTP 400 Bad Request).</exception>
    public async Task<AboutUsAssetForResultDto> AddAsync(AboutUsAssetForCreationDto dto)
    {
        var aboutUs = await _aboutUsRepository.SelectAll()
            .Where(au => au.Id == dto.AboutUsId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (aboutUs is null)
            throw new CustomException(404, "AboutUs is not found");

        var WwwRootPath = Path.Combine(EnvoronmentHelper.WebRootPath, "AboutUs", "AboutUsAsset");
        var assetsFolderPath = Path.Combine(WwwRootPath, "AboutUs");
        var ImagesFolderPath = Path.Combine(assetsFolderPath, "AboutUsAsset");

        if (!Directory.Exists(assetsFolderPath))
        {
            Directory.CreateDirectory(assetsFolderPath);
        }

        if (!Directory.Exists(ImagesFolderPath))
        {
            Directory.CreateDirectory(ImagesFolderPath);
        }
        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.Image.FileName);

        var fullPath = Path.Combine(WwwRootPath, fileName);

        using (var stream = File.OpenWrite(fullPath))
        {
            await dto.Image.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        string resultImage = Path.Combine("AboutUs", "AboutUsAsset", fileName);

        var mapped = _mapper.Map<AboutUsAsset>(dto);
        mapped.Image = resultImage;

        var result = await _aboutUsAssetRepository.InsertAsync(mapped);

        return _mapper.Map<AboutUsAssetForResultDto>(result);
    }

    /// <summary>
    /// Retrieves all non-deleted assets associated with the About Us section from the database.
    /// </summary>
    /// <returns>
    /// A task representing the collection of AboutUsAsset information in the form of AboutUsAssetForResultDto.
    /// </returns>
    public async Task<IEnumerable<AboutUsAssetForResultDto>> RetrieveAllAsync()
    {
        var assets = await _aboutUsAssetRepository.SelectAll()
            .Where(a => a.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        
        return _mapper.Map<IEnumerable<AboutUsAssetForResultDto>>(assets);
    }

    /// <summary>
    /// Retrieves an About Us asset from the database based on the provided asset ID.
    /// </summary>
    /// <param name="id">The unique identifier of the About Us asset to be retrieved.</param>
    /// <returns>
    /// A task representing the retrieved AboutUsAsset information in the form of AboutUsAssetForResultDto.
    /// </returns>
    /// <exception cref="CustomException">Thrown if the About Us asset with the specified ID is not found (HTTP 404 Not Found).</exception>
    public async Task<AboutUsAssetForResultDto> RetrieveByIdAsync(long id)
    {
        var asset = await _aboutUsAssetRepository.SelectAll()
            .Where(a => a.Id == id)
            .Include(a => a.AboutUs)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (asset == null)
            throw new CustomException(404,"AboutUsAsset is not found");

        return _mapper.Map<AboutUsAssetForResultDto>(asset);
    }

    /// <summary>
    /// Modifies an existing About Us asset in the database based on the provided asset ID and update information.
    /// </summary>
    /// <param name="id">The unique identifier of the About Us asset to be modified.</param>
    /// <param name="dto">The data transfer object containing the updated information for the About Us asset.</param>
    /// <returns>
    /// A task representing the modified AboutUsAsset information in the form of AboutUsAssetForResultDto.
    /// </returns>
    /// <exception cref="CustomException">Thrown if the About Us asset with the specified ID is not found or marked as deleted (HTTP 404 Not Found),
    /// or if the image format is not valid (HTTP 400 Bad Request).</exception>
    public async Task<AboutUsAssetForResultDto> ModifyAsync(long id, AboutUsAssetForUpdateDto dto)
    {
        var aboutUs = await _aboutUsRepository.SelectAll()
             .Where(e => e.Id == dto.AboutUsId)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (aboutUs is null)
            throw new CustomException(404, "AboutUs is not found");

        var abourUsAsset = await _aboutUsAssetRepository.SelectAll()
            .Where(aa => aa.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (abourUsAsset is null)
            throw new CustomException(404, "AboutUsAsset is not found");

        var fullPath = Path.Combine(EnvoronmentHelper.WebRootPath, abourUsAsset.Image);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }


        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.Image.FileName);
        var rootPath = Path.Combine(EnvoronmentHelper.WebRootPath, "AboutUs", "AboutUsAsset", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await dto.Image.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }
        string resultImage = Path.Combine("AboutUs", "AboutUsAsset", fileName);

        var mapped = _mapper.Map(dto, abourUsAsset);
        mapped.Image = resultImage;
        mapped.UpdatedAt = DateTime.UtcNow;

        var result = await _aboutUsAssetRepository.UpdateAsync(mapped);

        return _mapper.Map<AboutUsAssetForResultDto>(result);
    }

    /// <summary>
    /// Removes an existing About Us asset from the database based on the provided asset ID.
    /// </summary>
    /// <param name="id">The unique identifier of the About Us asset to be removed.</param>
    /// <returns>
    /// A task representing the success of the removal operation (true if successful).
    /// </returns>
    /// <exception cref="CustomException">Thrown if the About Us asset with the specified ID is not found (HTTP 404 Not Found).</exception>
    public async Task<bool> RemoveAsync(long id)
    {
        var asset = await _aboutUsAssetRepository.SelectAll()
             .Where(ea => ea.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (asset is null)
            throw new CustomException(404, "Event Asset is not found");

        var fullPath = Path.Combine(EnvoronmentHelper.WebRootPath, asset.Image);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return await _aboutUsAssetRepository.DeleteAsync(id);
    }
}
