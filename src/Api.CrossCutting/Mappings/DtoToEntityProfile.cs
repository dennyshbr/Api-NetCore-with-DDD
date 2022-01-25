using Api.Domain.Entities;
using AutoMapper;
using Domain.DTOs.User;

namespace CrossCutting.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<UserDto, UserEntity>()
                .ReverseMap();

            CreateMap<UserDtoCreate, UserEntity>()
                .ReverseMap();

            CreateMap<UserDtoUpdate, UserEntity>()
                .ReverseMap();

            CreateMap<UserCreateResultDTO, UserEntity>()
                .ReverseMap();

            CreateMap<UserUpdateResultDTO, UserEntity>()
                .ReverseMap();
        }
    }
}
