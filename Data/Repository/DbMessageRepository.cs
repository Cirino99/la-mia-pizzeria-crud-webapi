using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_static.Data.Repository
{
    public class DbMessageRepository : IMessageRepository
    {
        PizzeriaDbContext db;
        public DbMessageRepository(PizzeriaDbContext _db)
        {
            db = _db;
        }
        public List<Message> All()
        {
            return db.Messages.ToList();
        }
        public Message GetById(int id)
        {
            return db.Messages.Where(m => m.Id == id).FirstOrDefault();
        }
        public void Create(Message message)
        {
            db.Messages.Add(message);
            db.SaveChanges();
        }
    }
}
