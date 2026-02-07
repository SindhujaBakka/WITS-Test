using AutoMapper;
using Models.Dtos;
using Models.Entities;

namespace Services.Profiles
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AccountUser, AccountUserDto>();
        }
    }
}
