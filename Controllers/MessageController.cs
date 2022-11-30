using la_mia_pizzeria_static.Data.Repository;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("[controller]/[action]/{id?}", Order = 0)]
    public class MessageController : Controller
    {
        IMessageRepository messageRepository;
        public MessageController(IMessageRepository _messageRepository)
        {
            messageRepository = _messageRepository;
        }
        public IActionResult Index()
        {
            List<Message> messages = messageRepository.All(); 
            return View(messages);
        }
        public IActionResult Detail(int id)
        {
            Message message = messageRepository.GetById(id);
            if (message == null)
                return View("NotFound", "Il messaggio cercato non è stato trovato");

            return View(message);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Message message = messageRepository.GetById(id);

            if (message == null)
                return View("NotFound", "Il messaggio cercato non è stato trovato");

            messageRepository.Delete(message);

            return RedirectToAction("Index");
        }
    }
}
