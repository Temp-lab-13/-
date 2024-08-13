using AutoMapper;
using WATask1.DadaBase;

namespace WATask1.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserDto, User>()
                .ForMember(d => d.Id, op => op.Ignore())
                .ForMember(d => d.Email, op => op.MapFrom(q => q.Email))
                .ForMember(d => d.Name, op => op.MapFrom(q => q.Name))
                .ForMember(d => d.Surname, op => op.MapFrom(q => q.FamilyName))
                .ForMember(d => d.Registration, op => op.Ignore())
                .ForMember(d => d.Active, op => op.Ignore())
                .ForMember(d => d.Password, op => op.Ignore()).ReverseMap();
        }
    }
}
