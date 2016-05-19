using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using mycantina.DataAccess.Models;
using mycantina.Services;
using mycantina.UI.ViewModels.Consumer;
using System.Net;
using SharpRepository.EfRepository;

namespace mycantina.UI.Controllers
{
    public class ConsumerController : Controller
    {
        private MyCantinaDbContext _context;
        private EfRepository<Consumer> _consumerRepository;
        private ConsumerApplicationService _consumerApplicationService;

        public ConsumerController()
        {
            _context = new MyCantinaDbContext();
            _consumerRepository = new EfRepository<Consumer>(_context);
            _consumerApplicationService = new ConsumerApplicationService(_consumerRepository);
        }

        // GET: Consumer / Index
        public ActionResult Index()
        {
            var consumers = _consumerRepository.GetAll();
            var model = consumers.Select(c => new ConsumerIndexViewModel()
            {
                Id = c.Id,
                FullName = c.FirstName + " " + c.LastName,
                Email = c.Email
            }).ToList();

            return View(model);
        }

        // GET: Consumer / Create
        public ActionResult Create()
        {
            var model = new ConsumerCreateViewModel();

            return View(model);
        }

        // POST: Consumer / Create
        [HttpPost]
        public ActionResult Create(ConsumerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _consumerApplicationService.CreateConsumer(model.FirstName, model.MiddleNames, model.LastName, model.DateOfBirth, model.Email);
                    RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            return View(model);
        }

        // GET: Consumer / Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumer = _consumerRepository.Get(id.Value);

            if (consumer == null)
            {
                return HttpNotFound();
            }

            var model = new ConsumerEditViewModel()
            {
                Id = consumer.Id,
                FirstName = consumer.FirstName,
                MiddleNames = consumer.MiddleNames,
                LastName = consumer.LastName,
                Email = consumer.Email,
                DateOfBirth = consumer.DateOfBirth
            };

            return View(model);
        }

        // POST: Consumer / Edit
        [HttpPost]
        public ActionResult Edit(ConsumerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _consumerApplicationService.UpdateConsumer(model.Id, model.FirstName, model.MiddleNames, model.LastName, model.DateOfBirth, model.Email);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            return View(model);
        }

        // GET: Consumer / Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumer = _consumerRepository.Get(id.Value);

            if (consumer == null)
            {
                return HttpNotFound();
            }

            var model = new ConsumerDetailsViewModel()
            {
                Id = consumer.Id,
                FirstName = consumer.FirstName,
                MiddleNames = consumer.MiddleNames,
                LastName = consumer.LastName,
                Email = consumer.Email,
                DateOfBirth = consumer.DateOfBirth
            };

            return View(model);
        }

        // GET: Consumer / Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consumer = _consumerRepository.Get(id.Value);

            if (consumer == null)
            {
                return HttpNotFound();
            }

            var model = new ConsumerDetailsViewModel()
            {
                Id = consumer.Id,
                FirstName = consumer.FirstName,
                MiddleNames = consumer.MiddleNames,
                LastName = consumer.LastName,
                Email = consumer.Email,
                DateOfBirth = consumer.DateOfBirth
            };

            return View(model);
        }

        // POST: Consumer / Delete
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
                    _consumerApplicationService.RemoveConsumer(id.Value);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex);
                }
            }

            var consumer = _consumerRepository.Get(id.Value);

            if (consumer == null)
            {
                return HttpNotFound();
            }

            var model = new ConsumerDetailsViewModel()
            {
                Id = consumer.Id,
                FirstName = consumer.FirstName,
                MiddleNames = consumer.MiddleNames,
                LastName = consumer.LastName,
                Email = consumer.Email,
                DateOfBirth = consumer.DateOfBirth
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