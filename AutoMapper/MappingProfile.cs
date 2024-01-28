using AutoMapper;
using Nava.Dto;
using Nava.Entities;

namespace Nava.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Music, MusicDto>();
            CreateMap<Music, MusicContentDto>();
            CreateMap<UserInfo, UserInfoDto>();
            CreateMap<UserInfo, UpdateUserInfoDto>();
        }
    }
}
