using la_mia_pizzeria_static.Data.Repository;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IMessageRepository messageRepository;
        public MessageController(IMessageRepository _messageRepository)
        {
            messageRepository = _messageRepository;
        }
        [HttpPost]
        public IActionResult Create([FromBody] Message message)
        {
            messageRepository.Create(message);
            return Ok(message);
        }
    }
}
