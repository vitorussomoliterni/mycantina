using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.Services
{
    class UserApplicationService
    {
        MyCantinaDbContext _context;

        public UserApplicationService(MyCantinaDbContext context)
        {
            _context = context;
        }

        public User CreateUser(string firstName, string middleNames, string lastName, DateTime DateOfBirth, string email)
        {
            var user = new User()
            {
                FirstName = firstName,
                MiddleNames = middleNames,
                LastName = lastName,
                DateOfBirth = DateOfBirth,
                Email = email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User UpdateUser(int id, string firstName, string middleNames, string lastName, DateTime DateOfBirth, string email)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new InvalidOperationException("No user found for the provided id.");
            }

            user.FirstName = firstName;
            user.MiddleNames = middleNames;
            user.LastName = lastName;
            user.DateOfBirth = DateOfBirth;
            user.Email = email;

            _context.SaveChanges();

            return user;
        }

        public void RemoveUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new InvalidOperationException("No user found for the provided id.");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
