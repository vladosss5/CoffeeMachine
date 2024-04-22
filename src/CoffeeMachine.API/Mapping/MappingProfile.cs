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
        CreateMap<Banknote, BanknoteDto>().ReverseMap();
        
        CreateMap<CoffeeForOrderReq, Coffee>().ReverseMap();
        CreateMap<CoffeeForOrderResp, Coffee>().ReverseMap();
        CreateMap<CoffeeReq, Coffee>().ReverseMap();
        CreateMap<CoffeeRespForAdmin, Coffee>().ReverseMap();
        CreateMap<CoffeeAddReq, Coffee>().ReverseMap();
        CreateMap<CoffeeAddResp, Coffee>().ReverseMap();
        CreateMap<CoffeeUpdateDto, Coffee>().ReverseMap();
        
        CreateMap<MachineRespForAdmin, Machine>().ReverseMap();
        CreateMap<MachineAddReq, Machine>().ReverseMap();
        CreateMap<MachineAddResp, Machine>().ReverseMap();
        CreateMap<MachineReq, Machine>().ReverseMap();
        CreateMap<MachineResp, Machine>().ReverseMap();
        CreateMap<MachineForOrderDto, Machine>().ReverseMap();
        CreateMap<MachineUpdateDto, Machine>().ReverseMap();
        
        CreateMap<OrderAddReq, Order>().ReverseMap();
        CreateMap<OrderAddResp, Order>().ReverseMap();
        CreateMap<OrderRespForAdmin, Order>().ReverseMap();
        
        CreateMap<TransactionForOrderDto, Transaction>().ReverseMap();
    }
}