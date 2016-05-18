using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using mycantina.DataAccess.Models;
using mycantina.Services;
using mycantina.UI.ViewModels.Bottle;
using System.Net;

namespace mycantina.UI.Controllers
{
    public class BottleController : Controller
    {
        private MyCantinaDbContext _context;
        private BottleApplicationService _bottleApplicationServie;
        
        public BottleController()
        {
            _context = new MyCantinaDbContext();
            _bottleApplicationServie = new BottleApplicationService(_context);
        }

        // GET: Bottle / Index
        public ActionResult Index()
        {
            var bottles = _context.Bottles.Include(b => b.GrapeVarietyBottles).ToList();
            var model = _context.Bottles.Select(b => new BottleIndexViewModel
            {
                Id = b.Id,
                Name = b.Name,
                GrapeVariety = b.GrapeVarietyBottles.FirstOrDefault(g => g.BottleId == b.Id).GrapeVariety.Name,
                WineType = b.WineType.Name,
                Year = b.Year,
                Producer = b.Producer,
                AvgPrice = (b.MinPrice + b.MaxPrice) / 2 // TODO: Create a better logic to calculate the bottle's average price
            }).ToList();

            return View(model);
        }

        // GET: Bottle / Create
        public ActionResult Create()
        {
            var model = new BottleCreateViewModel();
            
            model.Regions = new SelectList(_context.Regions, "Id", "Name");
            //model.Countries = new SelectList(_context.Countries, "Id", "Name");
            model.WineTypes = new SelectList(_context.WineTypes, "Id", "Name");
            model.GrapeVarieties = new SelectList(_context.GrapeVarieties, "Id", "Name");

            return View(model);
        }

        // POST: Bottle / Create
        [HttpPost]
        public ActionResult Create(BottleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bottleApplicationServie.AddBottle(model.Name, model.RegionId, model.WineTypeId, model.Year, model.Producer, model.Description, model.GrapeVarietyId);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            model.Regions = new SelectList(_context.Regions, "Id", "Name", model.RegionId);
            //model.Countries = new SelectList(_context.Countries, "Id", "Name", model.CountryId);
            model.WineTypes = new SelectList(_context.WineTypes, "Id", "Name", model.WineTypeId);
            model.GrapeVarieties = new SelectList(_context.GrapeVarieties, "Id", "Name", model.GrapeVarietyId);

            return View(model);
        }

        // GET: Bottle / Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bottle = _context.Bottles.Include(b => b.GrapeVarietyBottles).FirstOrDefault(b => b.Id == id);

            if (bottle == null)
            {
                return HttpNotFound();
            }
            
            var model = new BottleEditViewModel()
            {
                Id = bottle.Id,
                Name = bottle.Name,
                RegionId = bottle.RegionId,
                WineTypeId = bottle.WineTypeId,
                Year = bottle.Year,
                Producer = bottle.Producer,
                Description = bottle.Description,
                GrapeVarietyId = bottle.GrapeVarietyId
            };

            model.Regions = new SelectList(_context.Regions, "Id", "Name", model.RegionId);
            //model.Countries = new SelectList(_context.Countries, "Id", "Name", model.CountryId);
            model.WineTypes = new SelectList(_context.WineTypes, "Id", "Name", model.WineTypeId);
            model.GrapeVarieties = new SelectList(_context.GrapeVarieties, "Id", "Name", model.GrapeVarietyId);

            return View(model);
        }

        // POST: Bottle / Edit
        [HttpPost]
        public ActionResult Edit(BottleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bottleApplicationServie.UpdateBottle(model.Id, model.Name, model.RegionId, model.WineTypeId, model.Year, model.Producer, model.Description, model.GrapeVarietyId);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }

                //_bottleApplicationServie.UpdateBottle(model.Id, model.Name, model.Region, model.Country, model.Type, model.Year, model.Producer, model.Description, model.GrapeVarietyId);
                //return RedirectToAction("Index");
            }

            model.Regions = new SelectList(_context.Regions, "Id", "Name", model.RegionId);
            //model.Countries = new SelectList(_context.Countries, "Id", "Name", model.CountryId);
            model.WineTypes = new SelectList(_context.WineTypes, "Id", "Name", model.WineTypeId);
            model.GrapeVarieties = new SelectList(_context.GrapeVarieties, "Id", "Name", model.GrapeVarietyId);

            return View(model);
        }

        // GET: Bottle / Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bottle = _context.Bottles.Include(b => b.GrapeVarietyBottles).FirstOrDefault(b => b.Id == id);

            if (bottle == null)
            {
                return HttpNotFound();
            }

            var grapeVariety = _context.GrapeVarieties.Find(bottle.GrapeVarietyId);

            var model = new BottleDetailsViewModel()
            {
                Id = bottle.Id,
                Name = bottle.Name,
                //Country = bottle.Country.Name,
                Region = bottle.Region.Name,
                GrapeVariety = bottle.GrapeVarietyBottles.FirstOrDefault(g => g.BottleId == bottle.Id).GrapeVariety.Name,
                WineType = bottle.WineType.Name,
                Year = bottle.Year,
                Producer = bottle.Producer,
                AvgPrice = (bottle.MinPrice + bottle.MaxPrice) / 2, // TODO: Create a better logic to calculate the bottle's average price
                Description = bottle.Description
            };

            return View(model);
        }

        // GET: Bottle / Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bottle = _context.Bottles.FirstOrDefault(b => b.Id == id);
            var grapeVariety = _context.GrapeVarieties.Find(bottle.GrapeVarietyId);

            if (bottle == null || grapeVariety == null)
            {
                return HttpNotFound();
            }
            
            var model = new BottleDetailsViewModel()
            {
                Id = bottle.Id,
                Name = bottle.Name,
                //Country = bottle.Country.Name,
                Region = bottle.Region.Name,
                GrapeVariety = grapeVariety.Name,
                WineType = bottle.WineType.Name,
                Year = bottle.Year,
                Producer = bottle.Producer,
                AvgPrice = (bottle.MinPrice + bottle.MaxPrice) / 2, // TODO: Create a better logic to calculate the bottle's average price
                Description = bottle.Description
            };

            return View(model);
        }

        // POST: Bottle / Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bottleApplicationServie.RemoveBottle(id.Value);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            var bottle = _context.Bottles.Include(b => b.GrapeVarietyId).FirstOrDefault(b => b.Id == id);

            if (bottle == null)
            {
                return HttpNotFound();
            }

            var grapeVariety = _context.GrapeVarieties.Find(bottle.GrapeVarietyId);

            var model = new BottleDetailsViewModel()
            {
                Id = bottle.Id,
                Name = bottle.Name,
                //Country = bottle.Country.Name,
                Region = bottle.Region.Name,
                GrapeVariety = grapeVariety.Name,
                WineType = bottle.WineType.Name,
                Year = bottle.Year,
                Producer = bottle.Producer,
                AvgPrice = (bottle.MinPrice + bottle.MaxPrice) / 2, // TODO: Create a better logic to calculate the bottle's average price
                Description = bottle.Description
            };

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
                _context = null;
            }

            base.Dispose(disposing);
        }
    }
}