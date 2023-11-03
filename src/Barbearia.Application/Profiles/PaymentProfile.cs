using AutoMapper;
using Barbearia.Application.Features.Payments.Commands.CreatePayment;
using Barbearia.Application.Features.Payments.Commands.UpdatePayment;
using Barbearia.Application.Features.Payments.Queries.GetPayment;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Payment, GetPaymentDto>().ReverseMap();

        CreateMap<CreatePaymentCommand, Payment>();        
        CreateMap<Payment, CreatePaymentDto>();  

        CreateMap<UpdatePaymentCommand, Payment>(); 
        CreateMap<Payment,UpdatePaymentDto>();       
    }
}