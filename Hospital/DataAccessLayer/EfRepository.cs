using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.DataAccessLayer
{
    public class EfRepository : IRepository
    {
        private HospitalContext db = new HospitalContext();

        public void CreateDoctor(Doctor doctor)
        {
            db.Doctors.Add(doctor);
            db.SaveChanges();
        }

        public void CreatePatient(Patient patient)
        {
            db.Patients.Add(patient);
            db.SaveChanges();
        }

        public void DeleteDoctor(int? id)
        {
            db.Doctors.Remove(GetDoctor(id));
            db.SaveChanges();
        }

        public void DeletePatient(int? id)
        {
            db.Patients.Remove(GetPatient(id));
            db.SaveChanges();
        }

        public Doctor GetDoctor(int? id)
        {
            return db.Doctors.Find(id);
        }

        public List<Doctor> GetDoctors()
        {
            return db.Doctors.ToList<Doctor>();
        }

        public List<Doctor> GetDoctors(string searchStringName)
        {
            return db.Doctors.Where(d => d.Name.Contains(searchStringName)).ToList<Doctor>();
        }

        public Patient GetPatient(int? id)
        {
            return db.Patients.Find(id);
        }

        public List<Patient> GetPatients()
        {
            return db.Patients.ToList<Patient>();
        }

        public List<Patient> GetPatients(string searchString)
        {
            return db.Patients.Where(p => p.Name.Contains(searchString)).ToList<Patient>();
        }

        public IEnumerable<Doctor> GetSelectedDoctors(int idPatient)
        {
            return GetPatient(idPatient).Doctors;
        }

        public void UpdateDoctor(Doctor doctor)
        {
            db.Entry(doctor).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdatePatient(Patient patient)
        {
            db.Entry(patient).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}