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
        private List<string> regionsList = new List<string>()
            {
                "South Australia",
                "Tuscany"
            };

        private List<string> countriesList = new List<string>()
            {
                "Australia",
                "Italy"
            };

        private List<string> typesList = new List<string>()
            {
                "Sparkling",
                "White",
                "Red",
                "Rosé",
                "Sweet Wine"
            };

        public BottleController()
        {
            _context = new MyCantinaDbContext();
            _bottleApplicationServie = new BottleApplicationService(_context);
        }

        // GET: Bottle / Index
        public ActionResult Index()
        {
            var bottles = _context.Bottles.ToList();
            var grapeVarietiesBottles = _context.GrapeVarietyBottles.ToList();
            var model = _context.Bottles.Include(b => b.GrapeVarietyBottles).Select(b => new BottleIndexViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Country = b.Country,
                GrapeVariety = grapeVarietiesBottles.FirstOrDefault(g => g.BottleId == b.Id).GrapeVariety.Name,
                Type = b.Type,
                Year = b.Year,
                Producer = b.Producer,
                AvgPrice = (b.MinPrice + b.MaxPrice) / 2 // TODO: Create a better logic to calculate the bottle's average price
            }).ToList();

            return View(model);
        }

        // GET Bottle / Create
        public ActionResult Create()
        {
            var model = new BottleCreateViewModel();



            model.Regions = new SelectList(regionsList);
            model.Countries = new SelectList(countriesList);
            model.Types = new SelectList(typesList);
            model.GrapeVarieties = new SelectList(_context.GrapeVarieties, "Id", "Name");

            return View(model);
        }

        // POST Bottle / Create
        [HttpPost]
        public ActionResult Create(BottleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bottleApplicationServie.AddBottle(model.Name, model.Region, model.Country, model.Type, model.Year, model.Producer, model.Description, model.GrapeVarietyId);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            model.Regions = new SelectList(regionsList, model.Region);
            model.Countries = new SelectList(countriesList, model.Region);
            model.Types = new SelectList(typesList, model.Region);
            model.GrapeVarieties = new SelectList(_context.GrapeVarieties, "Id", "Name", model.GrapeVarietyId);

            return View(model);
        }

        // GET Bottle / Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bottle = _context.Bottles.Include(b => b.GrapeVarietyId).FirstOrDefault(b => b.Id == id);

            if (bottle == null)
            {
                return HttpNotFound();
            }
            
            var model = new BottleEditViewModel()
            {
                Id = bottle.Id,
                Name = bottle.Name,
                Region = bottle.Region,
                Country = bottle.Country,
                Type = bottle.Type,
                Year = bottle.Year,
                Producer = bottle.Producer,
                Description = bottle.Description,
                GrapeVarietyId = bottle.GrapeVarietyId
            };

            model.Regions = new SelectList(regionsList, model.Region);
            model.Countries = new SelectList(countriesList, model.Region);
            model.Types = new SelectList(typesList, model.Region);
            model.GrapeVarieties = new SelectList(_context.GrapeVarieties, "Id", "Name", model.GrapeVarietyId);

            return View(model);
        }

        // POST Bottle / Edit
        [HttpPost]
        public ActionResult Edit(BottleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bottleApplicationServie.UpdateBottle(model.Id, model.Name, model.Region, model.Country, model.Type, model.Year, model.Producer, model.Description, model.GrapeVarietyId);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            model.Regions = new SelectList(regionsList, model.Region);
            model.Countries = new SelectList(countriesList, model.Region);
            model.Types = new SelectList(typesList, model.Region);
            model.GrapeVarieties = new SelectList(_context.GrapeVarieties, "Id", "Name", model.GrapeVarietyId);

            return View(model);
        }

        // GET Bottle / Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                Country = bottle.Country,
                Region = bottle.Region,
                GrapeVariety = grapeVariety.Name,
                Type = bottle.Type,
                Year = bottle.Year,
                Producer = bottle.Producer,
                AvgPrice = (bottle.MinPrice + bottle.MaxPrice) / 2, // TODO: Create a better logic to calculate the bottle's average price
                Description = bottle.Description
            };

            return View(model);
        }

        // GET Bottle / Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                Country = bottle.Country,
                Region = bottle.Region,
                GrapeVariety = grapeVariety.Name,
                Type = bottle.Type,
                Year = bottle.Year,
                Producer = bottle.Producer,
                AvgPrice = (bottle.MinPrice + bottle.MaxPrice) / 2, // TODO: Create a better logic to calculate the bottle's average price
                Description = bottle.Description
            };

            return View(model);
        }

        // POST Bottle / Delete
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
                Country = bottle.Country,
                Region = bottle.Region,
                GrapeVariety = grapeVariety.Name,
                Type = bottle.Type,
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