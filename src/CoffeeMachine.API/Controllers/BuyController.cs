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
        private readonly IBuyService _buyService;

        public BuyController(IMapper mapper, IBuyService buyService)
        {
            _mapper = mapper;
            _buyService = buyService;
        }

        [HttpPost]
        public async Task<IActionResult> Buy([FromBody] PurchaseRequest request)
        {
            Purchase purchase = new Purchase()
            {
                Coffee = new Coffee()
                {
                    Name = request.Coffee.Name,
                    Price = request.Coffee.Price
                },
                Machine = new Machine()
                {
                    SerialNumber = request.Machine.SerialNumber
                },
            };

            foreach (var banknote in request.Banknotes)
            {
                var transaction = new Transaction()
                {
                    Banknote = new Banknote()
                    {
                        Par = banknote.Par
                    },
                    Type = true,
                };
                purchase.Transactions.Add(transaction);
            }
            
            
            var buy = _buyService.BuyAsync(purchase);
            
            return Ok(buy);
        }
    }
}
