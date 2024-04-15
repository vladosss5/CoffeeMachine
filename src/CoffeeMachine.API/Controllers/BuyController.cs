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
        private readonly IOrderService _orderService;

        public BuyController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Buy([FromBody] OrderRequest request)
        {
            List<Transaction> transactions = new List<Transaction>();
            
            foreach (var banknote in request.Banknotes)
            {
                var transaction = new Transaction()
                {
                    Banknote = new Banknote()
                    {
                        Nominal = banknote.Nominal
                    },
                    Type = true,
                };
                transactions.Add(transaction);
            }
            
            Order order = new Order()
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
                Transactions = transactions
            };
            
            
            var buy = _orderService.CreateOrderAsync(order);
            
            return Ok(buy);
        }
    }
}
