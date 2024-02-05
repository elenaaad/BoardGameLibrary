using AutoMapper;
using DAL.Models.DTO;
using DAL.Models;

namespace BoardGameLibrary.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAuthRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}