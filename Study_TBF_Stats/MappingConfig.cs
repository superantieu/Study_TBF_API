using AutoMapper;
using Study_TBF_Stats.Models;
using Study_TBF_Stats.Models.Dto;

namespace Study_TBF_Stats
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<TbProject, TbProjectDto>()
               .ForMember(dest => dest.UsedHours, opt => opt.MapFrom(src => 0))
                .ReverseMap();
                config.CreateMap<TbUser, TbUserDto>()
                .ReverseMap();
            });
            return mappingConfig;
        }
    }
}
