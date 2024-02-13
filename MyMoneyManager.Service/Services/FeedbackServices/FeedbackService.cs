using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Service.Commons.CollectionExtensions;
using MyMoneyManager.Service.DTOs.Feedbacks;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.IFeedbacks;

namespace MyMoneyManager.Service.Services.FeedbackServices;

public class FeedbackService : IFeedbackService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Feedback> _repository;
    private readonly IRepository<User> _userRepository;

    public FeedbackService(IMapper mapper, IRepository<Feedback> repository, IRepository<User> userRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<FeedbackForResultDto> AddAsync(FeedbackForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CustomException(404,"User not found");

        var feedback = _mapper.Map<Feedback>(dto);
        feedback.CreatedAt = DateTime.UtcNow;
        var insertFeedback = await _repository.InsertAsync(feedback);

        var result = _mapper.Map<FeedbackForResultDto>(insertFeedback);
        return result;
    }


    public async Task<bool> DeleteAysnc(long id)
    {
        var feedback = await _repository.SelectAll()
            .Where (f => f.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (feedback is null)
            throw new CustomException(404, "Feedback not found");

        await _repository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<FeedbackForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var feedbacks = await _repository.SelectAll()
            .ToPagedList(@params)
             .AsNoTracking()
             .ToListAsync();

        return _mapper.Map<IEnumerable<FeedbackForResultDto>>(feedbacks);
    }
}
