using AutoMapper;
using CoffeeMachine.API.DTOs.Banknote;
using CoffeeMachine.API.DTOs.BanknoteToMachine;
using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Order;
using CoffeeMachine.API.DTOs.Transaction;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.API.Mapping;

/// <summary>
/// Класс правил для автомапера.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Banknote, BanknoteDto>().ReverseMap();
        
        CreateMap<BanknoteToMachineDto, BanknoteToMachine>().ReverseMap();
        
        CreateMap<CoffeeForOrderRequestDto, Coffee>().ReverseMap();
        CreateMap<CoffeeForOrderResponseDto, Coffee>().ReverseMap();
        CreateMap<CoffeeDto, Coffee>().ReverseMap();
        
        CreateMap<MachineForOrderDto, Machine>().ReverseMap();
        CreateMap<MachineCreateDto, Machine>().ReverseMap();
        CreateMap<MachineDto, Machine>().ReverseMap();
        
        CreateMap<OrderAddRequestDto, Order>().ReverseMap();
        CreateMap<OrderAddResponseDto, Order>().ReverseMap();
        CreateMap<OrderResponseDto, Order>().ReverseMap();
        
        CreateMap<TransactionForOrderDto, Transaction>().ReverseMap();
    }
}