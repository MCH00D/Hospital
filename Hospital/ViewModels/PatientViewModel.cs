using Hospital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.ViewModels
{
    public class PatientViewModel
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

        [Display(Name = "Doctors")]
        public string[] SelectedDoctors { get; set; }
    }
}