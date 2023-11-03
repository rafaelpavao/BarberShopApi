using AutoMapper;
using Barbearia.Application.Features.Orders.Commands.CreateOrder;
using Barbearia.Application.Features.Orders.Commands.UpdateOrder;
using Barbearia.Application.Features.Orders.Queries.GetAllOrders;
using Barbearia.Application.Features.Orders.Queries.GetOrderById;
using Barbearia.Application.Features.Orders.Queries.GetOrderByNumber;
using Barbearia.Application.Features.OrdersCollection;
using Barbearia.Application.Models;
using Barbearia.Application.Models.Appointments;
using Barbearia.Application.Models.Orders;
using Barbearia.Application.Models.Products;
using Barbearia.Application.Models.StockHistories;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<UpdateOrderCommand, Order>();
        CreateMap<Order, UpdateOrderDto>();

        CreateMap<Order, GetOrdersCollectionDto>();
        CreateMap<Order, GetAllOrdersDto>();
        CreateMap<Order, GetOrderByIdDto>();
        CreateMap<Order, GetOrderByNumberDto>();

        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, CreateOrderDto>();

        CreateMap<PersonDto, Person>().ReverseMap();
        CreateMap<StockHistoryOrder, StockHistoryOrderDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Appointment, AppointmentForGetOrderDto>();
        CreateMap<Order, OrderForUpdateDto>().ReverseMap();
        CreateMap<UpdateOrderCommand, OrderForUpdateDto>().ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<Appointment, AppointmentDto>();

    }
}