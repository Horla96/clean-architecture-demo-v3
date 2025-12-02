using Application.Features.Product.Commands;
using AutoMapper;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateProductCommand, Domain.Entities.Product>()
                .ForMember(destination => destination.Description, source => source.MapFrom(src => src.Remarks)).ReverseMap();
        }
    }
}
