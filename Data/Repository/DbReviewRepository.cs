using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_static.Data.Repository
{
    public class DbReviewRepository : IReviewRepository
    {
        PizzeriaDbContext db;
        public DbReviewRepository(PizzeriaDbContext _db)
        {
            db = _db;
        }
        public Review GetById(int id)
        {
            return db.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }
        public List<Review> GetList(int id)
        {
            return db.Reviews.Where(r => r.PizzaId == id).ToList();
        }
        public void Create(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
        }
        public void Update(Review review, Review formReview)
        {
            review.Name = formReview.Name;
            review.Text = formReview.Text;
            db.SaveChanges();
        }
        public void Delete(Review review)
        {
            db.Reviews.Remove(review);
            db.SaveChanges();
        }
    }
}
