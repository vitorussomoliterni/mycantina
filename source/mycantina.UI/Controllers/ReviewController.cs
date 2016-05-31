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
using SharpRepository.EfRepository;

namespace mycantina.UI.Controllers
{
    public class ReviewController : Controller
    {
        private MyCantinaDbContext _context;
        private ReviewApplicationService _reviewApplicationService;
        private EfRepository<Review> _reviewRepository;
        private EfRepository<Consumer> _consumerRepository;
        private EfRepository<Bottle> _bottleRepository;

        public ReviewController()
        {
            _context = new MyCantinaDbContext();
            _reviewRepository = new EfRepository<Review>(_context);
            _consumerRepository = new EfRepository<Consumer>(_context);
            _bottleRepository = new EfRepository<Bottle>(_context);
            _reviewApplicationService = new ReviewApplicationService(_reviewRepository,_consumerRepository,_bottleRepository);
        }

        // GET: Review / Index
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reviews = _reviewRepository.AsQueryable().Where(r => r.ConsumerId == id);
            
            var model = reviews.Select(r => new ReviewIndexViewModel()
            {
                ConsumerId = id.Value,
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

            var consumer = _consumerRepository.Get(consumerId.Value);
            var bottle = _bottleRepository.Get(bottleId.Value);

            if (consumer == null || bottle == null)
            {
                return HttpNotFound();
            }

            var model = new ReviewCreateViewModel()
            {
                ConsumerId = consumerId.Value,
                BottleId = bottleId.Value
            };

            model.Ratings = new SelectList(new List<int>{ 1, 2, 3, 4, 5 });

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
                    return RedirectToAction("Details", new { consumerId = model.ConsumerId, bottleId = model.BottleId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            model.Ratings = new SelectList(new List<int> { 1, 2, 3, 4, 5 });

            return View(model);
        }

        // GET: Review / Edit
        public ActionResult Edit(int? consumerId, int? bottleId)
        {
            if (consumerId == null || bottleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumer = _consumerRepository.Get(consumerId.Value);
            var bottle = _bottleRepository.Get(bottleId.Value);
            var review = _reviewRepository.Find(r => r.ConsumerId == consumerId.Value && r.BottleId == bottleId.Value);

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

            model.Ratings = new SelectList(new List<int> { 1, 2, 3, 4, 5 });

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
                    return RedirectToAction("Details", new { consumerId = model.ConsumerId, bottleId = model.BottleId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            model.Ratings = new SelectList(new int[] { 1, 2, 3, 4, 5 });

            return View(model);
        }

        // GET: Review / Details
        public ActionResult Details(int? consumerId, int? bottleId)
        {
            if (consumerId == null || bottleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumer = _consumerRepository.Get(consumerId.Value);
            var bottle = _bottleRepository.Get(bottleId.Value);
            var review = _reviewRepository.Find(r => r.ConsumerId == consumerId.Value && r.BottleId == bottleId.Value);

            if (consumer == null || bottle == null || review == null)
            {
                return HttpNotFound();
            }

            var model = new ReviewDetailsViewModel()
            {
                ConsumerId = consumerId.Value,
                BottleId = bottleId.Value,
                BottleName = bottle.Name,
                //Country = bottle.Country.Name,
                Region = bottle.Region.Name,
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

            var consumer = _consumerRepository.Get(consumerId.Value);
            var bottle = _bottleRepository.Get(bottleId.Value);
            var review = _reviewRepository.Find(r => r.ConsumerId == consumerId.Value && r.BottleId == bottleId.Value);

            if (consumer == null || bottle == null || review == null)
            {
                return HttpNotFound();
            }

            var model = new ReviewDetailsViewModel()
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
                    return RedirectToAction("Index", new { id = consumerId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            var consumer = _consumerRepository.Get(consumerId.Value);
            var bottle = _bottleRepository.Get(bottleId.Value);
            var review = _reviewRepository.Find(r => r.ConsumerId == consumerId.Value && r.BottleId == bottleId.Value);

            if (consumer == null || bottle == null || review == null)
            {
                return HttpNotFound();
            }

            var model = new ReviewDetailsViewModel()
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