using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRepository.Repository;


namespace mycantina.Services
{
    public class ReviewApplicationService
    {
        private IRepository<Review> _reviewRepository;
        private IRepository<Consumer> _consumerRepository;
        private IRepository<Bottle> _bottleRepository;

        public ReviewApplicationService(IRepository<Review> reviewRepository, IRepository<Consumer> consumerRepository, IRepository<Bottle> bottleRepository)
        {
            _reviewRepository = reviewRepository;
            _consumerRepository = consumerRepository;
            _bottleRepository = bottleRepository;
        }

        public Review AddReview(int consumerId, int bottleId, string text, int rating)
        {
            var review = new Review()
            {
                ConsumerId = consumerId,
                BottleId = bottleId,
                Text = text,
                Rating = rating,
                DatePosted = DateTime.Now
            };

            var consumer = _consumerRepository.Get(consumerId);
            var bottle = _bottleRepository.Get(bottleId);
            
            consumer.Reviews.Add(review);
            review.Consumer = consumer;
            
            bottle.Reviews.Add(review);
            review.Bottle = bottle;

            _reviewRepository.Add(review);

            return review;
        }

        public Review UpdateReview(int consumerId, int bottleId, string text, int rating)
        {
            var review = _reviewRepository.Reviews.FirstOrDefault(r => r.ConsumerId == consumerId && r.BottleId == bottleId);

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

        public void RemoveReview(int consumerId, int bottleId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.ConsumerId == consumerId && r.BottleId == bottleId);

            if (review == null)
            {
                throw new InvalidOperationException("No review found for the provided parameters.");
            }

            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}
