using AutoMapper;
using OrderAPI.Dto;
using OrderAPI.Entities;

namespace OrderAPI.Utils
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.FullAdress,
                    opt => opt.MapFrom(x => string.Join(' ', x.Address.AddressLine, x.Address.Country, x.Address.City, x.Address.CityCode)))
                .ForMember(o => o.ProductProperties,
                    opt => opt.MapFrom(x => string.Join(' ', x.Product.Id, x.Product.Name, x.Product.ImageUrl)));
            CreateMap<Address, AddressDto>();
        }
    }
}
