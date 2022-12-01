using la_mia_pizzeria_static.Data.Repository;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]", Order = 1)]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        IReviewRepository reviewRepository;
        public ReviewController(IReviewRepository _reviewRepository)
        {
            reviewRepository = _reviewRepository;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Review review = reviewRepository.GetById(id);
            return Ok(review);
        }
        [HttpGet("{id}")]
        public IActionResult GetList(int id)
        {
            List<Review> review = reviewRepository.GetList(id);
            return Ok(review);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Review review)
        {
            reviewRepository.Create(review);
            return Ok(review);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Review formReview)
        {
            Review review = reviewRepository.GetById(id);
            
            if (review != null)
            {
                reviewRepository.Update(review, formReview);
                return Ok(review);
            }
            else
            {
                return NotFound();
            }
          
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Review review = reviewRepository.GetById(id);

            if (review != null)
            {
                reviewRepository.Delete(review);
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
