using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital.DataAccessLayer
{
    public class HospitalContext : DbContext
    {
        public HospitalContext() : base("Hospital")
        {
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }
    }
}