using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public enum PatientStatus
    {
        Arrived,
        Sick,
        Healthy
    }

    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PatientStatus Status { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Day of birth")]
        public DateTime DayOfBirth { get; set; }

        [Display(Name = "Tax code")]
        public string TaxCode { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }

        public Patient()
        {
            Doctors = new List<Doctor>();
        }
    }
}