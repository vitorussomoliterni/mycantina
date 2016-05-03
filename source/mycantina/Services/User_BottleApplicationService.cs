using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.Services
{
    public class User_BottleApplicationService
    {
        private MyCantinaDbContext _context;

        public User_BottleApplicationService(MyCantinaDbContext context)
        {
            _context = context;
        }

        public User_Bottle AddUser_Bottle(int userId, int bottleId, DateTime? dateAcquired, DateTime? dateOpened, int qtyOwned, bool owned, decimal pricePaid, int wineFormatId)
        {
            var user_bottle = new User_Bottle()
            {
                UserId = userId,
                BottleId = bottleId,
                DateAcquired = dateAcquired,
                DateOpened = dateOpened,
                QtyOwned = qtyOwned,
                Owned = owned,
                PricePaid = pricePaid,
                WineFormatId = wineFormatId
            };

            _context.User_Bottles.Add(user_bottle);
            _context.SaveChanges();

            return user_bottle;
        }

        public User_Bottle UpdateUser_Bottle(int id, DateTime? dateAcquired, DateTime? dateOpened, int qtyOwned, bool owned, decimal pricePaid)
        {
            var user_bottle = _context.User_Bottles.Find(id);

            if (user_bottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            user_bottle.DateAcquired = dateAcquired;
            user_bottle.DateOpened = dateOpened;
            if (dateOpened != null) // If a user updates the bottle to signify that it was drank, the QtyOwned will be updated
            {
                user_bottle.QtyOwned--;
            }
            user_bottle.QtyOwned = qtyOwned;
            user_bottle.Owned = owned;
            user_bottle.PricePaid = pricePaid;

            _context.SaveChanges();

            return user_bottle;
        }

        public void RemoveUser_Bottle(int id)
        {
            var user_bottle = _context.User_Bottles.Find(id);

            if (user_bottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            _context.User_Bottles.Remove(user_bottle);
            _context.SaveChanges();
        }
    }
}
