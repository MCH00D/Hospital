using Hospital.DataAccessLayer;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class DoctorsController : Controller
    {
        private IRepository repository;

        public DoctorsController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ViewResult Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(repository.GetDoctors(searchString));
            }

            return View(repository.GetDoctors());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Doctor doctor = repository.GetDoctor(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }

            return View(doctor);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Specialization")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                repository.CreateDoctor(doctor);
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Doctor doctor = repository.GetDoctor(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }

            return View(doctor);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Doctor doctor = repository.GetDoctor(id);
            if (TryUpdateModel(doctor, "",
               new string[] { "Name", "Specialization" }))
            {
                repository.UpdateDoctor(doctor);
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Doctor doctor = repository.GetDoctor(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }

            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            repository.DeleteDoctor(id);
            return RedirectToAction("Index");
        }
    }
}