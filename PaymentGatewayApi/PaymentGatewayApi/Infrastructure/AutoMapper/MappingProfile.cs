using AutoMapper;
using PaymentGatewayApi.Domain;
using PaymentGatewayApi.Models.v1;

namespace PaymentGatewayApi.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePaymentModel, Payment>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
