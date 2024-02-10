using AutoMapper;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Domain.Entities.AboutUs;
using MyMoneyManager.Service.DTOs.AboutUs;
using MyMoneyManager.Service.DTOs.AboutUsAssets;
using MyMoneyManager.Service.DTOs.Feedbacks;
using MyMoneyManager.Service.DTOs.Goals;
using MyMoneyManager.Service.DTOs.Users;

namespace MyMoneyManager.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Users
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();

        // AboutUs
        CreateMap<AboutUs, AboutUsForCreationDto>().ReverseMap();
        CreateMap<AboutUs, AboutUsForUpdateDto>().ReverseMap();
        CreateMap<AboutUs, AboutUsForResultDto>().ReverseMap();

        // AboutUsAssetg
        CreateMap<AboutUsAsset, AboutUsAssetForCreationDto>().ReverseMap();
        CreateMap<AboutUsAsset, AboutUsAssetForResultDto>().ReverseMap();
        CreateMap<AboutUsAsset, AboutUsAssetForUpdateDto>().ReverseMap();

        // Feedback
        CreateMap<Feedback,FeedbackForCreationDto>().ReverseMap();
        CreateMap<Feedback,FeedbackForResultDto>().ReverseMap();
        CreateMap<Feedback,FeedbackForUpdateDto>().ReverseMap();

        // Goal
        CreateMap<Goal,GoalForCreationDto>().ReverseMap();
        CreateMap<Goal,GoalForResultDto>().ReverseMap();
        CreateMap<Goal,GoalForUpdateDto>().ReverseMap();
        
    }
}
