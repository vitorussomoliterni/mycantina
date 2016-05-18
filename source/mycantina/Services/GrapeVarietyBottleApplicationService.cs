using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SharpRepository.Repository;
using mycantina.DataAccess.Models;

namespace mycantina.Services
{
    public class GrapeVarietyBottleApplicationService
    {
        private IRepository<GrapeVarietyBottle> _grapeVarietyBottleRepository;

        public GrapeVarietyBottleApplicationService(IRepository<GrapeVarietyBottle> grapeVarietyBottleRepository)
        {
            _grapeVarietyBottleRepository = grapeVarietyBottleRepository;
        }

        public GrapeVarietyBottle AddGrapeVarietyBottle(int bottleId, int grapeVarietyId, Bottle bottle, GrapeVariety grapeVariety)
        {
            var grapeVarietyBottle = new GrapeVarietyBottle
            {
                BottleId = bottle.Id,
                GrapeVarietyId = grapeVarietyId,
                Bottle = bottle,
                GrapeVariety = grapeVariety
            };

            _grapeVarietyBottleRepository.Add(grapeVarietyBottle);

            return grapeVarietyBottle;
        }

        public GrapeVarietyBottle RemoveGrapeVarietyBottle(int bottleId, int grapeVarietyId)
        {
            // TODO: Implement logic
            return null;
        }
    }
}
