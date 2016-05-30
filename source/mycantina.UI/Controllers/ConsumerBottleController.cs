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
using SharpRepository.EfRepository;

namespace mycantina.UI.Controllers
{
    public class ConsumerBottleController : Controller
    {
        private MyCantinaDbContext _context;
        private EfRepository<ConsumerBottle> _consumerBottleRepository;
        private EfRepository<Consumer> _consumerRepository;
        private EfRepository<Bottle> _bottleRepository;
        private EfRepository<WineFormat> _wineFormatRepository;
        private ConsumerBottleApplicationService _consumerBottleApplicationService;

        public ConsumerBottleController()
        {
            _context = new MyCantinaDbContext();
            _consumerBottleRepository = new EfRepository<ConsumerBottle>(_context);
            _consumerRepository = new EfRepository<Consumer>(_context);
            _bottleRepository = new EfRepository<Bottle>(_context);
            _wineFormatRepository = new EfRepository<WineFormat>(_context);
            _consumerBottleApplicationService = new ConsumerBottleApplicationService(_consumerBottleRepository, _consumerRepository);
        }

        // GET: ConsumerBottle / Index
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumerBottles = _consumerBottleRepository.AsQueryable().Where(c => c.ConsumerId == id);

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

            var consumer = _consumerRepository.Get(consumerId.Value);
            var bottle = _bottleRepository.Get(bottleId.Value);

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
                    return RedirectToAction("Index", new { id = model.ConsumerId });
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

            var ConsumerBottle = _consumerBottleRepository.Get(id.Value);

            if (ConsumerBottle == null)
            {
                return HttpNotFound();
            }

            var model = new ConsumerBottleEditViewModel()
            {
                Id = ConsumerBottle.Id,
                ConsumerId = ConsumerBottle.ConsumerId,
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
                    return RedirectToAction("Details", new { id = model.ConsumerId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            model.WineFormats = new SelectList(_context.WineFormats, "Id", "Name", model.WineFormatId);

            return View(model);
        }

        // GET: ConsumerBottle / Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumerBottle = _consumerBottleRepository.Get(id.Value);

            if (consumerBottle == null)
            {
                return HttpNotFound();
            }

            var wineFormat = _wineFormatRepository.Get(consumerBottle.WineFormatId);

            if (wineFormat == null)
            {
                return HttpNotFound();
            }

            var model = new ConsumerBottleDetailsViewModel()
            {
                Id = consumerBottle.Id,
                ConsumerId = consumerBottle.ConsumerId,
                BottleId = consumerBottle.BottleId,
                DateAcquired = consumerBottle.DateAcquired,
                DateOpened = consumerBottle.DateOpened,
                Owned = consumerBottle.Owned,
                PricePaid = consumerBottle.PricePaid,
                QtyOwned = consumerBottle.QtyOwned,
                WineFormat = wineFormat.Name
            };

            return View(model);
        }

        // GET: ConsumerBottle / Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumerBottle = _consumerBottleRepository.Get(id.Value);

            if (consumerBottle == null)
            {
                return HttpNotFound();
            }

            var model = new ConsumerBottleDetailsViewModel()
            {
                Id = consumerBottle.Id,
                ConsumerId = consumerBottle.ConsumerId,
                DateAcquired = consumerBottle.DateAcquired,
                DateOpened = consumerBottle.DateOpened,
                Owned = consumerBottle.Owned,
                PricePaid = consumerBottle.PricePaid,
                QtyOwned = consumerBottle.QtyOwned,
                WineFormat = consumerBottle.WineFormat.Name
            };

            return View(model);
        }

        // POST: ConsumerBottle / Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumerBottle = _consumerBottleRepository.Get(id.Value);

            if (consumerBottle == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _consumerBottleApplicationService.RemoveConsumerBottle(id.Value);
                    return RedirectToAction("Index", new { id = consumerBottle.ConsumerId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            var model = new ConsumerBottleDetailsViewModel()
            {
                Id = consumerBottle.Id,
                ConsumerId = consumerBottle.ConsumerId,
                DateAcquired = consumerBottle.DateAcquired,
                DateOpened = consumerBottle.DateOpened,
                Owned = consumerBottle.Owned,
                PricePaid = consumerBottle.PricePaid,
                QtyOwned = consumerBottle.QtyOwned,
                WineFormat = consumerBottle.WineFormat.Name
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