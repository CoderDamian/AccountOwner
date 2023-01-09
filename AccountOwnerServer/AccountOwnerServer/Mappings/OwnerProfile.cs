using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace AccountOwnerServer.Mappings
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<Owner, OwnerDto>()
                .ReverseMap();

            CreateMap<Account, AccountDTO>()
                .ReverseMap();

            CreateMap<Owner, OwnerForCreationDto>()
                .ReverseMap();

            CreateMap<Owner, OwnerForUpdateDto>()
                .ReverseMap();
        }
    }
}
