using AutoMapper;
using CoffeeMachine.API.DTOs;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Machine, MachineDto>().ReverseMap();
        CreateMap<Coffee, CoffeeDto>().ReverseMap();
        CreateMap<Banknote, BanknoteDto>().ReverseMap();
        CreateMap<Transaction, TransactionDto>().ReverseMap();
    }
}