using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hospital.DataAccessLayer
{
    public interface IRepository
    {
        List<Doctor> GetDoctors();

        List<Doctor> GetDoctors(string searchString);

        Doctor GetDoctor(int? id);

        void CreateDoctor(Doctor doctor);

        void UpdateDoctor(Doctor doctor);

        void DeleteDoctor(int? id);


        List<Patient> GetPatients();

        List<Patient> GetPatients(string searchString);

        Patient GetPatient(int? id);

        void CreatePatient(Patient patient);

        void UpdatePatient(Patient patient);

        void DeletePatient(int? id);

        IEnumerable<Doctor> GetSelectedDoctors(int idPatient);

    }
}
