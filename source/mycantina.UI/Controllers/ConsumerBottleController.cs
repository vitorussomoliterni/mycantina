using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using mycantina.DataAccess.Models;
using mycantina.Services;
using mycantina.UI.ViewModels.ConsumerBottle;
using System.Net;

namespace mycantina.UI.Controllers
{
    public class ConsumerBottleController : Controller
    {
        private MyCantinaDbContext _context;
        private ConsumerBottleApplicationService _consumerBottleApplicationService;

        public ConsumerBottleController()
        {
            _context = new MyCantinaDbContext();
            _consumerBottleApplicationService = new ConsumerBottleApplicationService(_context);
        }

        // GET: ConsumerBottle / Index
        public ActionResult Index(int? consumerId)
        {
            if (consumerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumerBottles = _context.ConsumerBottles.Include(c => c.Consumer).Include(c => c.Bottle).Where(c => c.Consumer.Id == consumerId).ToList();
            var model = consumerBottles.Select(c => new ConsumerBottleIndexViewModel()
            {
                Id = c.Id,
                ConsumerId = c.Consumer.Id,
                WineName = c.Bottle.Name,
                WineFormat = c.WineFormat.Name,
                Owned = c.Owned,
                QtyOwned = c.QtyOwned,
                PricePaid = c.PricePaid
            }).ToList();

            return View(model);
        }

        // GET: ConsumerBottle / Create
        public ActionResult Create(int? consumerId, int? bottleId)
        {
            if (consumerId == null || bottleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumer = _context.Consumers.Find(consumerId.Value);
            var bottle = _context.Bottles.Find(bottleId.Value);

            if (consumer == null || bottle == null)
            {
                return HttpNotFound();
            }

            var model = new ConsumerBottleCreateViewModel()
            {
                ConsumerId = consumerId.Value,
                BottleId = bottleId.Value
            };

            model.WineFormats = new SelectList(_context.WineFormats, "Id", "Name");

            return View(model);
        }

        // POST: ConsumerBottle / Create
        [HttpPost]
        public ActionResult Create(ConsumerBottleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _consumerBottleApplicationService.AddConsumerBottle(model.ConsumerId, model.BottleId, model.DateAcquired, model.DateOpened, model.QtyOwned, model.Owned, model.PricePaid, model.WineFormatId);
                    return RedirectToAction("Index//{0}", model.ConsumerId);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            model.WineFormats = new SelectList(_context.WineFormats, "Id", "Name", model.WineFormatId);

            return View(model);
        }

        // GET: ConsumerBottle / Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ConsumerBottle = _context.ConsumerBottles.Find(id.Value);

            if (ConsumerBottle == null)
            {
                return HttpNotFound();
            }

            var model = new ConsumerBottleEditViewModel()
            {
                Id = ConsumerBottle.Id,
                DateAcquired = ConsumerBottle.DateAcquired,
                DateOpened = ConsumerBottle.DateOpened,
                Owned = ConsumerBottle.Owned,
                PricePaid = ConsumerBottle.PricePaid,
                QtyOwned = ConsumerBottle.QtyOwned,
                WineFormatId = ConsumerBottle.WineFormatId
            };

            model.WineFormats = new SelectList(_context.WineFormats, "Id", "Name", model.WineFormatId);

            return View(model);
        }

        // POST: ConsumerBottle / Edit
        [HttpPost]
        public ActionResult Edit(ConsumerBottleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _consumerBottleApplicationService.UpdateConsumerBottle(model.Id, model.DateAcquired, model.DateOpened, model.QtyOwned, model.Owned, model.PricePaid);
                    return RedirectToAction("Details//{0}", model.Id);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            model.WineFormats = new SelectList(_context.WineFormats, "Id", "Name", model.WineFormatId);

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