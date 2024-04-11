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
        private readonly IBuyService _buyService;

        public BuyController(IPurchaseService purchaseService, IMapper mapper, IBuyService buyService)
        {
            _mapper = mapper;
            _purchaseService = purchaseService;
            _buyService = buyService;
        }

        [HttpPost]
        public async Task<IActionResult> Buy([FromBody] PurchaseRequest request)
        {
            var buy = _mapper.Map<PurchaseResponce>(
                _buyService.BuyAsync(
                    _mapper.Map<Purchase>(request)));
            
            return Ok(buy);
        }
    }
}
