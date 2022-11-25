using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Core.Models;

namespace TaskManager.WebApi.Mapper
{
    public class DefaultAutoMapperProfiles : Profile
    {
        public DefaultAutoMapperProfiles()
        {
            CreateMap<AppUser, AppUserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<AppUserDto, AppUser>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new Role { Name = src.Role }));

            CreateMap<CronTask, CronTaskDto>();
            CreateMap<CronTaskDto, CronTask>();
        }
    }
}
