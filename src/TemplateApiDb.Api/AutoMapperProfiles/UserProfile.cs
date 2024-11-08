using AutoMapper;

namespace TemplateApiDb.Api.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            _ = CreateMap<Domain.Entities.User, DTO.User>()
                .ForMember(d => d.Username, d => d.MapFrom(x => x.Username.Value))
                .ForMember(d => d.FirstName, d => d.MapFrom(x => x.FirstName.Value))
                .ForMember(d => d.LastName, d => d.MapFrom(x => x.LastName.Value))
                .ForMember(d => d.Email, d => d.MapFrom(x => x.Email.Value))
                .ForMember(d => d.PhoneNumber, d => d.MapFrom(x => x.PhoneNumber.Value))
                .ReverseMap();
        }
    }
}
