using AutoMapper;
using CoffeeMachine.API.DTO;
using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BuyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseService _purchaseService;

        public BuyController(IPurchaseService purchaseService, IMapper mapper)
        {
            _mapper = mapper;
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<IActionResult> Buy([FromBody] PurchaseRequest request)
        {
            var purechase = _purchaseService.AddAsync(_mapper.Map<Purchase>(request)).Result;
            
            return Ok(purechase);
        }
    }
}
