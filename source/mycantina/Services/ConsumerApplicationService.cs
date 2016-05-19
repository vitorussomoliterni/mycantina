using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRepository.Repository;

namespace mycantina.Services
{
    public class ConsumerApplicationService
    {
        private IRepository<Consumer> _consumerRepository;

        public ConsumerApplicationService(IRepository<Consumer> consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        public Consumer CreateConsumer(string firstName, string middleNames, string lastName, DateTime DateOfBirth, string email)
        {
            var consumer = new Consumer()
            {
                FirstName = firstName,
                MiddleNames = middleNames,
                LastName = lastName,
                DateOfBirth = DateOfBirth,
                Email = email
            };

            _consumerRepository.Add(consumer);

            return consumer;
        }

        public Consumer UpdateConsumer(int id, string firstName, string middleNames, string lastName, DateTime DateOfBirth, string email)
        {
            var consumer = _consumerRepository.Get(id);

            if (consumer == null)
            {
                throw new InvalidOperationException("No consumer found for the provided id.");
            }

            consumer.FirstName = firstName;
            consumer.MiddleNames = middleNames;
            consumer.LastName = lastName;
            consumer.DateOfBirth = DateOfBirth;
            consumer.Email = email;

            _consumerRepository.Update(consumer);

            return consumer;
        }

        public void RemoveConsumer(int id)
        {
            var consumer = _consumerRepository.Get(id);

            if (consumer == null)
            {
                throw new InvalidOperationException("No consumer found for the provided id.");
            }

            _consumerRepository.Delete(consumer);
        }
    }
}
