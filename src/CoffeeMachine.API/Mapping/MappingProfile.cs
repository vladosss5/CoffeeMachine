using AutoMapper;
using CoffeeMachine.API.DTOs.Banknote;
using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Order;
using CoffeeMachine.API.DTOs.Transaction;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderAddReq, Order>().ReverseMap();
        CreateMap<OrderAddResp, Order>().ReverseMap();
        CreateMap<TransactionForOrderDto, Transaction>().ReverseMap();
        CreateMap<MachineForOrderDto, Machine>().ReverseMap();
        CreateMap<CoffeeForOrderReq, Coffee>().ReverseMap();
        CreateMap<CoffeeForOrderResp, Coffee>().ReverseMap();
        CreateMap<Banknote, BanknoteDto>().ReverseMap();

        CreateMap<CoffeeRespForAdmin, Coffee>().ReverseMap();
        CreateMap<MachineRespForAdmin, Machine>().ReverseMap();
        CreateMap<OrderRespForAdmin, Order>().ReverseMap();
    }
}