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
        private IRepository<GrapeVariety> _grapeVarietyRepository;
        private IRepository<GrapeVarietyBottle> _grapeVarietyBottleRepository;

        public BottleApplicationService(IRepository<Bottle> bottleRepository, IRepository<GrapeVariety> grapeVarietyRepository, IRepository<GrapeVarietyBottle> grapeVarietyBottleRepository)
        {
            _bottleRepository = bottleRepository;
            _grapeVarietyRepository = grapeVarietyRepository;
            _grapeVarietyBottleRepository = grapeVarietyBottleRepository;
        }

        public Bottle AddBottle(string name, int regionId, int wineTypeId, int year, string producer, string description, int grapeVarietyId)
        {
            var grapeVariety = _grapeVarietyRepository.Get(grapeVarietyId);

            var bottle = new Bottle()
            {
                Name = name,
                RegionId = regionId,
                Description = description,
                WineTypeId = wineTypeId,
                Year = year,
                Producer = producer,
                GrapeVarietyId = grapeVariety.Id
            };

            var grapeVarietyBottle = new GrapeVarietyBottle
            {
                BottleId = bottle.Id,
                GrapeVarietyId = grapeVarietyId,
                Bottle = bottle,
                GrapeVariety = grapeVariety
            };

            bottle.GrapeVarietyBottles.Add(grapeVarietyBottle);

            _grapeVarietyBottleRepository.Add(grapeVarietyBottle);
            _bottleRepository.Add(bottle);

            return bottle;
        }

        public Bottle UpdateBottle(int id, string name, int regionId, int wineTypeId, int year, string producer, string description, int grapeVarietyId)
        {
            var bottle = _context.Bottles.Find(id);
            var grapeVarietyBottle = _context.GrapeVarietyBottles.FirstOrDefault(g => g.GrapeVarietyId == bottle.GrapeVarietyId && g.BottleId == bottle.Id);

            if (bottle == null || grapeVarietyBottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            bottle.Name = name;
            bottle.RegionId = regionId;
            bottle.WineTypeId = wineTypeId;
            bottle.Year = year;
            bottle.Producer = producer;
            bottle.Description = description;
            bottle.GrapeVarietyId = grapeVarietyId;
            
            grapeVarietyBottle.Bottle = bottle;
            grapeVarietyBottle.GrapeVarietyId = bottle.GrapeVarietyId;

            _context.SaveChanges();

            return bottle;
        }

        public void RemoveBottle(int id)
        {
            var bottle = _context.Bottles.Find(id);

            if (bottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            _context.Bottles.Remove(bottle);
            _context.SaveChanges();
        }
    }
}
