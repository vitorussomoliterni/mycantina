using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using mycantina.DataAccess.Models;
using mycantina.Services;
using System.Net;
using mycantina.UI.ViewModels.Review;

namespace mycantina.UI.Controllers
{
    public class ReviewController : Controller
    {
        private MyCantinaDbContext _context;
        private ReviewApplicationService _reviewApplicationService;

        public ReviewController()
        {
            _context = new MyCantinaDbContext();
            _reviewApplicationService = new ReviewApplicationService(_context);
        }

        // GET: Review / Index
        public ActionResult Index(int? consumerId)
        {
            if (consumerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reviews = _context.Reviews.Where(r => r.ConsumerId == consumerId).Include(r => r.Bottle).ToList();
            var model = reviews.Select(r => new ReviewIndexViewModel()
            {
                ConsumerId = consumerId.Value,
                BottleId = r.BottleId,
                BottleName = r.Bottle.Name,
                Text = r.Text,
                Rating = r.Rating,
                DatePosted = r.DatePosted
            }).ToList();

            return View(model);
        }

        // GET: Review / Create
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

            var model = new ReviewCreateViewModel()
            {
                ConsumerId = consumerId.Value,
                BottleId = bottleId.Value
            };

            return View(model);
        }

        // POST: Review / Create
        [HttpPost]
        public ActionResult Create(ReviewCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _reviewApplicationService.AddReview(model.ConsumerId, model.BottleId, model.Text, model.Rating);
                    return RedirectToAction("Deatils//{0}//{1}", model.ConsumerId.ToString(), model.BottleId.ToString());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            return View(model);
        }

        // GET: Review / Edit
        public ActionResult Edit(int? consumerId, int? bottleId)
        {
            if (consumerId == null || bottleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumer = _context.Consumers.Find(consumerId.Value);
            var bottle = _context.Bottles.Find(bottleId.Value);
            var review = _context.Reviews.FirstOrDefault(r => r.ConsumerId == consumerId.Value && r.BottleId == bottleId.Value);

            if (consumer == null || bottle == null || review == null)
            {
                return HttpNotFound();
            }
            
            var model = new ReviewEditViewModel()
            {
                ConsumerId = consumerId.Value,
                BottleId = bottleId.Value,
                Rating = review.Rating,
                Text = review.Text
            };

            return View(model);
        }

        // POST: Review / Edit
        [HttpPost]
        public ActionResult Edit(ReviewEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _reviewApplicationService.UpdateReview(model.ConsumerId, model.BottleId, model.Text, model.Rating);
                    return RedirectToAction("Deatils//{0}//{1}", model.ConsumerId.ToString(), model.BottleId.ToString());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            return View(model);
        }

        // GET: Review / Details
        public ActionResult Details(int? consumerId, int? bottleId)
        {
            if (consumerId == null || bottleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumer = _context.Consumers.Find(consumerId.Value);
            var bottle = _context.Bottles.Include(b => b.GrapeVarietyBottles).FirstOrDefault(b => b.Id == bottleId.Value);
            var review = _context.Reviews.FirstOrDefault(r => r.ConsumerId == consumerId.Value && r.BottleId == bottleId.Value);

            if (consumer == null || bottle == null || review == null)
            {
                return HttpNotFound();
            }

            var model = new ReviewDetailsViewModel()
            {
                ConsumerId = consumerId.Value,
                BottleId = bottleId.Value,
                BottleName = bottle.Name,
                Country = bottle.Country.Name,
                Region = bottle.Region.Name,
                GrapeVariety = bottle.GrapeVarietyBottles.Find(g => g.GrapeVarietyId == bottle.GrapeVarietyId).GrapeVariety.Name,
                WineType = bottle.WineType.Name,
                Year = bottle.Year,
                Producer = bottle.Producer,
                AvgPrice = (bottle.MinPrice + bottle.MaxPrice) / 2, // TODO: Fix this stuff
                Description = bottle.Description,
                Text = review.Text,
                Rating = review.Rating,
                DatePosted = review.DatePosted,
                DateModified = review.DateModified
            };

            return View(model);
        }

        // GET: Review / Delete
        public ActionResult Delete(int? consumerId, int? bottleId)
        {
            if (consumerId == null || bottleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumer = _context.Consumers.Find(consumerId.Value);
            var bottle = _context.Bottles.Include(b => b.GrapeVarietyBottles).FirstOrDefault(b => b.Id == bottleId.Value);
            var review = _context.Reviews.FirstOrDefault(r => r.ConsumerId == consumerId.Value && r.BottleId == bottleId.Value);

            if (consumer == null || bottle == null || review == null)
            {
                return HttpNotFound();
            }

            var model = new ReviewIndexViewModel()
            {
                ConsumerId = consumerId.Value,
                BottleId = bottleId.Value,
                BottleName = review.Bottle.Name,
                Text = review.Text,
                Rating = review.Rating,
                DatePosted = review.DatePosted
            };

            return View(model);
        }

        // POST: Review / Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? consumerId, int? bottleId)
        {
            if (consumerId == null || bottleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _reviewApplicationService.RemoveReview(consumerId.Value, bottleId.Value);
                    return RedirectToAction("Index//{0}", consumerId.ToString());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            var consumer = _context.Consumers.Find(consumerId.Value);
            var bottle = _context.Bottles.Include(b => b.GrapeVarietyBottles).FirstOrDefault(b => b.Id == bottleId.Value);
            var review = _context.Reviews.FirstOrDefault(r => r.ConsumerId == consumerId.Value && r.BottleId == bottleId.Value);

            if (consumer == null || bottle == null || review == null)
            {
                return HttpNotFound();
            }

            var model = new ReviewIndexViewModel()
            {
                ConsumerId = consumerId.Value,
                BottleId = bottleId.Value,
                BottleName = review.Bottle.Name,
                Text = review.Text,
                Rating = review.Rating,
                DatePosted = review.DatePosted
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