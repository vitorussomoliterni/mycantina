using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.Services
{
    public class ConsumerApplicationService
    {
        private MyCantinaDbContext _context;

        public ConsumerApplicationService(MyCantinaDbContext context)
        {
            _context = context;
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

            _context.Consumers.Add(consumer);
            _context.SaveChanges();

            return consumer;
        }

        public Consumer UpdateConsumer(int id, string firstName, string middleNames, string lastName, DateTime DateOfBirth, string email)
        {
            var consumer = _context.Consumers.Find(id);

            if (consumer == null)
            {
                throw new InvalidOperationException("No consumer found for the provided id.");
            }

            consumer.FirstName = firstName;
            consumer.MiddleNames = middleNames;
            consumer.LastName = lastName;
            consumer.DateOfBirth = DateOfBirth;
            consumer.Email = email;

            _context.SaveChanges();

            return consumer;
        }

        public void RemoveConsumer(int id)
        {
            var consumer = _context.Consumers.Find(id);

            if (consumer == null)
            {
                throw new InvalidOperationException("No consumer found for the provided id.");
            }

            _context.Consumers.Remove(consumer);
            _context.SaveChanges();
        }
    }
}
