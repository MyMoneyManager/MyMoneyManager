using AutoMapper;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Service.DTOs.Users;

namespace MyMoneyManager.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Users
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();
    }
}
