using mycantina.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.Services
{
    public class BottleApplicationService
    {
        private MyCantinaDbContext _context;

        public BottleApplicationService(MyCantinaDbContext context)
        {
            _context = context;
        }

        public Bottle AddBottle(string name, string region, string country, string type, DateTime year, string producer, string description, int grapeVarietyId)
        {
            var grapeVariety = _context.GrapeVarieties.Find(grapeVarietyId);

            var bottle = new Bottle()
            {
                Name = name,
                Region = region,
                Country = country,
                Description = description,
                Type = type,
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

            _context.GrapeVarietyBottles.Add(grapeVarietyBottle);
            _context.Bottles.Add(bottle);
            _context.SaveChanges();

            return bottle;
        }

        public Bottle UpdateBottle(int id, string name, string region, string country, string type, DateTime year, string producer, string description, int grapeVarietyId)
        {
            var bottle = _context.Bottles.Find(id);
            var grapeVarietyBottle = _context.GrapeVarietyBottles.FirstOrDefault(g => g.GrapeVarietyId == bottle.GrapeVarietyId && g.BottleId == bottle.Id);

            if (bottle == null || grapeVarietyBottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            bottle.Name = name;
            bottle.Region = region;
            bottle.Country = country;
            bottle.Type = type;
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
