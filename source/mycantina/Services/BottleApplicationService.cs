using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpRepository.Repository;

namespace mycantina.Services
{
    public class BottleApplicationService
    {
        private IRepository<Bottle> _bottleRepository;

        public BottleApplicationService(IRepository<Bottle> bottleRepository)
        {
            _bottleRepository = bottleRepository;
        }

        public Bottle AddBottle(string name, int regionId, int wineTypeId, int year, string producer, string description, List<GrapeVariety> varieties)
        {
            var bottle = new Bottle()
            {
                Name = name,
                RegionId = regionId,
                Description = description,
                WineTypeId = wineTypeId,
                Year = year,
                Producer = producer,
                GrapeVarieties = varieties
            };
            
            _bottleRepository.Add(bottle);

            return bottle;
        }

        public Bottle UpdateBottle(int id, string name, int regionId, int wineTypeId, int year, string producer, string description, List<GrapeVariety> varieties)
        {
            var bottle = _bottleRepository.Get(id);


            if (bottle == null || varieties.Count > 0)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            if (varieties.Count > 0)
            {
                throw new InvalidOperationException("At least one grape variety needs to be selected.");
            }

            bottle.Name = name;
            bottle.RegionId = regionId;
            bottle.WineTypeId = wineTypeId;
            bottle.Year = year;
            bottle.Producer = producer;
            bottle.Description = description;
            bottle.GrapeVarieties = varieties;

            _bottleRepository.Update(bottle);

            return bottle;
        }

        public void RemoveBottle(int id)
        {
            var bottle = _bottleRepository.Get(id);

            if (bottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            _bottleRepository.Delete(bottle);
        }
    }
}
