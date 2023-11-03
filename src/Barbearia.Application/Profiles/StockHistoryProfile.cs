using AutoMapper;
using Barbearia.Application.Features.StockHistories.Commands.CreateStockHistory;
using Barbearia.Application.Features.StockHistories.Commands.UpdateStockHistory;
using Barbearia.Application.Features.StockHistories.Queries;
using Barbearia.Application.Features.StockHistories.Queries.GetStockHistoryById;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class StockHistoryProfile : Profile
{
    public StockHistoryProfile()
    {
        CreateMap<StockHistory, StockHistoryForSupplierDto>().ReverseMap();
        CreateMap<StockHistory, GetStockHistoryByIdDto>().ReverseMap();
        CreateMap<StockHistoryOrder, GetStockHistoryByIdDto>().ReverseMap();
        CreateMap<StockHistorySupplier, GetStockHistoryByIdDto>().ReverseMap();
        CreateMap<StockHistoryOrder, GetStockHistoryOrderDto>().ReverseMap();
        CreateMap<StockHistory, GetStockHistoryOrderDto>().ReverseMap();
        CreateMap<StockHistory, GetStockHistorySupplierDto>().ReverseMap();
        CreateMap<StockHistorySupplier, GetStockHistorySupplierDto>().ReverseMap();

        CreateMap<StockHistoryForSupplierDto, GetStockHistoryOrderDto>().ReverseMap();
        CreateMap<StockHistoryForSupplierDto, GetStockHistorySupplierDto>().ReverseMap();

        // CreateMap<StockHistory, GetAllStockHistoriesDto>().ReverseMap();
        CreateMap<StockHistory, CreateStockHistoryCommand>().ReverseMap();
        CreateMap<StockHistoryOrder, CreateStockHistoryCommand>().ReverseMap();
        CreateMap<StockHistorySupplier, CreateStockHistoryCommand>().ReverseMap();
        CreateMap<StockHistory, CreateStockHistoryDto>().ReverseMap();
        CreateMap<StockHistory,CreateStockHistoryOrderDto>().ReverseMap();
        CreateMap<StockHistory,CreateStockHistorySupplierDto>().ReverseMap();
        CreateMap<StockHistoryOrder,CreateStockHistoryOrderDto>().ReverseMap();
        CreateMap<StockHistorySupplier,CreateStockHistorySupplierDto>().ReverseMap();


        CreateMap<StockHistory, UpdateStockHistoryCommand>().ReverseMap();
        CreateMap<StockHistoryOrder, UpdateStockHistoryCommand>().ReverseMap();
        CreateMap<StockHistorySupplier, UpdateStockHistoryCommand>().ReverseMap();
    }
    
}