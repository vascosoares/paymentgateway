using AutoMapper;
using PaymentApi.Domain;
using PaymentApi.Models.v1;

namespace PaymentApi.Infrastructure.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PaymentModel, Payment>()
                .ForMember(x => x.PaymentState, opt => opt.MapFrom(src => 1));
        }
    }
}