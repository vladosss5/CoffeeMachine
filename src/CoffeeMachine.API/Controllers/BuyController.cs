using AutoMapper;
using CoffeeMachine.API.DTO;
using CoffeeMachine.Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BuyController : ControllerBase
    {
        // private readonly IMapper _mapper;
        private readonly IBanknoteService _banknoteService;

        public BuyController(IBanknoteService banknoteService)
        {
            // _mapper = mapper;
            _banknoteService = banknoteService;
        }
    }
}
