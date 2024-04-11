using AutoMapper;
using CoffeeMachine.API.DTO;
using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PurchaseRequest, Purchase>();
        // CreateMap<Purchase, PurchaseDto>();
        CreateMap<BanknoteDto, Banknote>();
        CreateMap<CoffeeDto, Coffee>();
        CreateMap<MachineDto, Machine>();
        
        CreateMap<Banknote, BanknoteDto>();
        CreateMap<Coffee, CoffeeDto>();
        CreateMap<Machine, MachineDto>();
    }
}