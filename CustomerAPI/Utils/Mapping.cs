using AutoMapper;
using CustomerAPI.Dto;
using CustomerAPI.Entities;

namespace CustomerAPI.Utils
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Address, AddressDto>();
        }
    }
}
