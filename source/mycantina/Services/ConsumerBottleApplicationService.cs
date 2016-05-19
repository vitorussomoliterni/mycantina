using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRepository.Repository;

namespace mycantina.Services
{
    public class ConsumerBottleApplicationService
    {
        private IRepository<ConsumerBottle> _consumerBottleRepository;

        public ConsumerBottleApplicationService(IRepository<ConsumerBottle> consumerBottleRepository)
        {
            _consumerBottleRepository = consumerBottleRepository;
        }

        public ConsumerBottle AddConsumerBottle(int consumerId, int bottleId, DateTime? dateAcquired, DateTime? dateOpened, int qtyOwned, bool owned, decimal pricePaid, int wineFormatId)
        {
            var consumerBottle = new ConsumerBottle()
            {
                ConsumerId = consumerId,
                BottleId = bottleId,
                DateAcquired = dateAcquired,
                DateOpened = dateOpened,
                QtyOwned = qtyOwned,
                Owned = owned,
                PricePaid = pricePaid,
                WineFormatId = wineFormatId
            };

            _consumerBottleRepository.Add(consumerBottle);

            return consumerBottle;
        }

        public ConsumerBottle UpdateConsumerBottle(int id, DateTime? dateAcquired, DateTime? dateOpened, int qtyOwned, bool owned, decimal pricePaid)
        {
            var consumerBottle = _consumerBottleRepository.Get(id);

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

            _consumerBottleRepository.Update(consumerBottle);

            return consumerBottle;
        }

        public void RemoveConsumerBottle(int id)
        {
            var consumerBottle = _consumerBottleRepository.Get(id);

            if (consumerBottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            _consumerBottleRepository.Delete(consumerBottle);
        }
    }
}
