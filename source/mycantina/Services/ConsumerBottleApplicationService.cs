using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.Services
{
    public class ConsumerBottleApplicationService
    {
        private MyCantinaDbContext _context;

        public ConsumerBottleApplicationService(MyCantinaDbContext context)
        {
            _context = context;
        }

        public ConsumerBottle AddConsumerBottle(DateTime? dateAcquired, DateTime? dateOpened, int qtyOwned, bool owned, decimal pricePaid, int wineFormatId)
        {
            var consumerBottle = new ConsumerBottle()
            {
                DateAcquired = dateAcquired,
                DateOpened = dateOpened,
                QtyOwned = qtyOwned,
                Owned = owned,
                PricePaid = pricePaid,
                WineFormatId = wineFormatId
            };

            _context.ConsumerBottles.Add(consumerBottle);
            _context.SaveChanges();

            return consumerBottle;
        }

        public ConsumerBottle UpdateConsumerBottle(int id, DateTime? dateAcquired, DateTime? dateOpened, int qtyOwned, bool owned, decimal pricePaid)
        {
            var consumerBottle = _context.ConsumerBottles.Find(id);

            if (consumerBottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            consumerBottle.DateAcquired = dateAcquired;
            consumerBottle.DateOpened = dateOpened;
            if (dateOpened != null) // If a consumer updates the bottle to signify that it was drank, the QtyOwned will be updated
            {
                consumerBottle.QtyOwned--;
            }
            consumerBottle.QtyOwned = qtyOwned;
            consumerBottle.Owned = owned;
            consumerBottle.PricePaid = pricePaid;

            _context.SaveChanges();

            return consumerBottle;
        }

        public void RemoveConsumerBottle(int id)
        {
            var consumerBottle = _context.ConsumerBottles.Find(id);

            if (consumerBottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            _context.ConsumerBottles.Remove(consumerBottle);
            _context.SaveChanges();
        }
    }
}
