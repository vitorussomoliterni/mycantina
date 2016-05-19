using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using mycantina.Services;
using SharpRepository.InMemoryRepository;
using mycantina.DataAccess.Models;

namespace mycantina.Tests
{
    public class ReviewApplicationServiceTests
    {
        [Fact]
        public void UpdateReviewTest_ShouldThrowInvalidOperationsExceptionIfReviewIsNull()
        {
            // Set up fixture
            InMemoryRepository<Review> reviewRepository = new InMemoryRepository<Review>();
            InMemoryRepository<Consumer> consumerRepository = new InMemoryRepository<Consumer>();
            InMemoryRepository<Bottle> bottleRepository = new InMemoryRepository<Bottle>();
            ReviewApplicationService reviewApplicationService = new ReviewApplicationService(reviewRepository, consumerRepository, bottleRepository);

            // Exercise SUT and verify outcome
            var ex = Assert.Throws<InvalidOperationException>(() => reviewApplicationService.UpdateReview(1, 1, "", 0));
            Assert.Equal("No review found for the provided parameters.", ex.Message);
        }

        [Fact]
        public void CreateReviewTest_ShouldCreateAReview()
        {
            // Set up fixture
            InMemoryRepository<Review> reviewRepository = new InMemoryRepository<Review>();
            InMemoryRepository<Consumer> consumerRepository = new InMemoryRepository<Consumer>();
            InMemoryRepository<Bottle> bottleRepository = new InMemoryRepository<Bottle>();
            ReviewApplicationService reviewApplicationService = new ReviewApplicationService(reviewRepository, consumerRepository, bottleRepository);
            var bottle = new Bottle() { Id = 1};
            var consumer = new Consumer() { Id = 1};
            bottleRepository.Add(bottle);
            consumerRepository.Add(consumer);
            var expectedText = "A review.";
            var expectedRating = 2;
            var expectedBottleId = bottle.Id;
            var expectedConsumerId = consumer.Id;

            // Exercise SUT
            reviewApplicationService.AddReview(consumer.Id, bottle.Id, expectedText, expectedRating);
            var actualReview = reviewRepository.Find(r => r.BottleId == bottle.Id && r.ConsumerId == consumer.Id);
            var actualText = actualReview.Text;
            var actualRating = actualReview.Rating;
            var actualBottleId = actualReview.BottleId;
            var actualConsumerId = actualReview.ConsumerId;

            // Verify outcome
            Assert.NotNull(actualReview);
            Assert.Equal(expectedText, actualText);
            Assert.Equal(expectedRating, actualRating);
            Assert.Equal(expectedBottleId, actualBottleId);
            Assert.Equal(expectedConsumerId, actualConsumerId);
        }
    }
}
