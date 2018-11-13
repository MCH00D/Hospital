using Hospital.DataAccessLayer;
using Hospital.Models;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class PatientsController : Controller
    {
        private IRepository repository;

        public PatientsController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ViewResult Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(repository.GetPatients(searchString));
            }

            return View(repository.GetPatients());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Patient patient = repository.GetPatient(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            PatientViewModel patientViewModel = new PatientViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                Status = patient.Status,
                DayOfBirth = patient.DayOfBirth,
                TaxCode = patient.TaxCode
            };

            ViewBag.SelectedDoctors = repository.GetSelectedDoctors(patient.Id);
            return View(patientViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AllDoctors = repository.GetDoctors();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Status, DayOfBirth, TaxCode, SelectedDoctors")] PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = new Patient
                {
                    Name = patientViewModel.Name,
                    Status = patientViewModel.Status,
                    DayOfBirth = patientViewModel.DayOfBirth,
                    TaxCode = patientViewModel.TaxCode
                };

                if (patientViewModel.SelectedDoctors != null)
                {
                    foreach (string item in patientViewModel.SelectedDoctors)
                    {
                        patient.Doctors.Add(repository.GetDoctor(Int32.Parse(item)));
                    }
                }

                repository.CreatePatient(patient);
                return RedirectToAction("Index");
            }

            return View(patientViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Patient patient = repository.GetPatient(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            PatientViewModel patientViewModel = new PatientViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                Status = patient.Status,
                DayOfBirth = patient.DayOfBirth,
                TaxCode = patient.TaxCode,
                SelectedDoctors = repository.GetSelectedDoctors(patient.Id).Select(i => i.Id.ToString()).ToArray()
            };

            ViewBag.AllDoctors = repository.GetDoctors();
            return View(patientViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Status, DayOfBirth, TaxCode, SelectedDoctors")] PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = repository.GetPatient(patientViewModel.Id);

                patient.Name = patientViewModel.Name;
                patient.Status = patientViewModel.Status;
                patient.DayOfBirth = patientViewModel.DayOfBirth;
                patient.TaxCode = patientViewModel.TaxCode;

                patient.Doctors.Clear();
                if (patientViewModel.SelectedDoctors != null)
                {
                    foreach (string item in patientViewModel.SelectedDoctors)
                    {
                        patient.Doctors.Add(repository.GetDoctor(Int32.Parse(item)));
                    }
                }

                repository.UpdatePatient(patient);
                return RedirectToAction("Index");
            }
            return View(patientViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Patient patient = repository.GetPatient(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            PatientViewModel patientViewModel = new PatientViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                Status = patient.Status,
                DayOfBirth = patient.DayOfBirth,
                TaxCode = patient.TaxCode
            };

            ViewBag.SelectedDoctors = repository.GetSelectedDoctors(patient.Id);
            return View(patientViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            repository.DeletePatient(id);
            return RedirectToAction("Index");
        }
    }
}
