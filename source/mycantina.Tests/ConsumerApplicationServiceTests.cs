using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SharpRepository.InMemoryRepository;
using mycantina.DataAccess.Models;
using mycantina.Services;

namespace mycantina.Tests
{
    public class ConsumerApplicationServiceTests
    {
        [Fact]
        public void CreateConsumerTest_ShouldCreateAValidConsumer()
        {
            // Set up fixture
            InMemoryRepository<Consumer> consumerRepository = new InMemoryRepository<Consumer>();
            ConsumerApplicationService consumerApplicationService = new ConsumerApplicationService(consumerRepository);
            var expectedFirstName = "Tizio";
            var expectedMiddleNames = "Caio";
            var expectedLastName = "Sempronio";
            var expectedDateOfBirth = new DateTime(1980, 1, 1);
            var expectedEmail = "tiziocaiosempronio@email.com";

            // Exercise SUT
            consumerApplicationService.CreateConsumer(expectedFirstName, expectedMiddleNames, expectedLastName, expectedDateOfBirth, expectedEmail);
            var actualConsumer = consumerRepository.Get(1);

            // Verify outcome
            Assert.NotNull(actualConsumer);
            Assert.Equal(expectedFirstName, actualConsumer.FirstName);
            Assert.Equal(expectedLastName, actualConsumer.LastName);
            Assert.Equal(expectedMiddleNames, actualConsumer.MiddleNames);
            Assert.Equal(expectedDateOfBirth, actualConsumer.DateOfBirth);
            Assert.Equal(expectedEmail, actualConsumer.Email);
        }

        [Fact]
        public void RemoveConsumerTest_ShouldThrowInvalidOperationExceptionIfConsumerIsNull()
        {
            // Set up fixture
            InMemoryRepository<Consumer> consumerRepository = new InMemoryRepository<Consumer>();
            ConsumerApplicationService consumerApplicationService = new ConsumerApplicationService(consumerRepository);
            const int id = 1;

            // Exercise SUT and verify outcome
            var ex = Assert.Throws<InvalidOperationException>(() => consumerApplicationService.RemoveConsumer(id));
            Assert.Equal("No consumer found for the provided id.", ex.Message);
        }
    }
}
