using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.Services
{
    class ReviewApplicationService
    {
        MyCantinaDbContext _context;

        public ReviewApplicationService(MyCantinaDbContext context)
        {
            _context = context;
        }

        public Review AddReview(int userId, int bottleId, string text, int rating)
        {
            var review = new Review()
            {
                UserId = userId,
                BottleId = bottleId,
                Text = text,
                Rating = rating,
                DatePosted = DateTime.Now
            };

            var user = _context.Users.Find(userId);
            var bottle = _context.Bottles.Find(bottleId);

            user.Reviews.Add(review);
            bottle.Reviews.Add(review);

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return review;
        }

        public Review UpdateReview(int userId, int bottleId, string text, int rating)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.UserId == userId && r.BottleId == bottleId);

            if (review == null)
            {
                throw new InvalidOperationException("No review found for the provided parameters.");
            }

            review.Text = text;
            review.Rating = rating;
            review.DateModified = DateTime.Now;

            _context.SaveChanges();

            return review;
        }

        public void RemoveReview(int userId, int bottleId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.UserId == userId && r.BottleId == bottleId);

            if (review == null)
            {
                throw new InvalidOperationException("No review found for the provided parameters.");
            }

            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}
