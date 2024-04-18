using AutoMapper;
using CoffeeMachine.API.DTOs;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderRequest>().ReverseMap();
        CreateMap<OrderResponse, Order>().ReverseMap();
        CreateMap<Machine, MachineDto>().ReverseMap();
        CreateMap<Coffee, CoffeeDto>().ReverseMap();
        CreateMap<Banknote, BanknoteDto>().ReverseMap();
        CreateMap<Transaction, TransactionDto>().ReverseMap();
    }
}