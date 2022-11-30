using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_static.Data.Repository
{
    public interface IMessageRepository
    {
        List<Message> All();
        Message GetById(int id);
        void Create(Message message);
        void Delete(Message message);
    }
}
