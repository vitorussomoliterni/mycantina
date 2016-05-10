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

        public Bottle AddBottle(string name, string region, string country, string type, DateTime year, string producer, string description, decimal minPrice, decimal maxPrice, int grapeVarietyId)
        {
            var bottle = new Bottle()
            {
                Name = name,
                Region = region,
                Country = country,
                Type = type,
                Year = year,
                Producer = producer,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            var grapeVariety = _context.GrapeVarieties.Find(grapeVarietyId);

            var grapeVariety_Bottle = new GrapeVarietyBottle
            {
                BottleId = bottle.Id,
                GrapeVarietyId = grapeVarietyId,
                Bottle = bottle,
                GrapeVariety = grapeVariety
            };

            bottle.GrapeVarietyBottles.Add(grapeVariety_Bottle);

            _context.GrapeVarietyBottles.Add(grapeVariety_Bottle);
            _context.Bottles.Add(bottle);
            _context.SaveChanges();

            return bottle;
        }

        public Bottle UpdateBottle(int id, decimal minPrice, decimal maxPrice)
        {
            var bottle = _context.Bottles.Find(id);

            if (bottle == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            bottle.MinPrice = minPrice;
            bottle.MaxPrice = maxPrice;

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
