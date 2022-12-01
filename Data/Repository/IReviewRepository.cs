using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_static.Data.Repository
{
    public interface IReviewRepository
    {
        Review GetById(int id);
        List<Review> GetList(int id);
        void Create(Review review);
        void Update(Review review, Review formReview);
        void Delete(Review review);
    }
}
