using AutoMapper;
using Nava.Dto;
using Nava.Entities;

namespace Nava.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Music, MusicDto>();
        }
    }
}
