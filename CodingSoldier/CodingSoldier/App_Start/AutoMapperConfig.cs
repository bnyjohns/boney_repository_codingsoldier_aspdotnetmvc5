using AutoMapper;
using CodingSoldier.Core.Entities;
using CodingSoldier.Core.Models;
using CodingSoldier.Models;

namespace CodingSoldier
{
    public class AutoMapperConfig
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.Initialize(mapperConfiguration =>
            {
                mapperConfiguration.CreateMap<Post, PostViewModel>().ReverseMap();
                mapperConfiguration.CreateMap<Study, StudyViewModel>().ReverseMap();
                mapperConfiguration.CreateMap<Post, PostApiEntity>().ReverseMap();
                mapperConfiguration.CreateMap<Study, StudyApiEntity>().ReverseMap();
            });
        }
    }
}