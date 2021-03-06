﻿using mycantina.DataAccess.Models;
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
        private IRepository<GrapeVariety> _varietyRepository;
        private IRepository<Region> _regionRepository;
        private IRepository<WineType> _wineTypeRepository;

        public BottleApplicationService(IRepository<Bottle> bottleRepository, IRepository<GrapeVariety> varietyRepository, IRepository<Region> regionRepository, IRepository<WineType> wineTypeRepository)
        {
            _bottleRepository = bottleRepository;
            _varietyRepository = varietyRepository;
            _regionRepository = regionRepository;
            _wineTypeRepository = wineTypeRepository;
        }

        public Bottle AddBottle(string name, int regionId, int wineTypeId, int year, string producer, string description, int[] varieties)
        {
            var varietiesDbEntities = _varietyRepository.AsQueryable()
                .Where(v => varieties.Contains(v.Id)).ToList();

            var bottle = new Bottle()
            {
                Name = name,
                RegionId = regionId,
                Description = description,
                WineTypeId = wineTypeId,
                Year = year,
                Producer = producer,
                GrapeVarieties = varietiesDbEntities
            };
            
            _bottleRepository.Add(bottle);

            return bottle;
        }

        public Bottle UpdateBottle(int id, string name, int regionId, int wineTypeId, int year, string producer, string description, int[] varieties)
        {
            var bottle = _bottleRepository.Get(id);
            var region = _regionRepository.Get(regionId);
            var wineType = _wineTypeRepository.Get(wineTypeId);
            var varietiesDbEntities = _varietyRepository.AsQueryable()
                .Where(v => varieties.Contains(v.Id)).ToList();


            if (bottle == null || region == null || wineType == null)
            {
                throw new InvalidOperationException("No bottle found for the provided id.");
            }

            if (varieties.Length < 0)
            {
                throw new InvalidOperationException("At least one grape variety needs to be selected.");
            }

            bottle.Name = name;
            bottle.Region = region;
            bottle.WineType = wineType;
            bottle.Year = year;
            bottle.Producer = producer;
            bottle.Description = description;
            bottle.GrapeVarieties = varietiesDbEntities;

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
