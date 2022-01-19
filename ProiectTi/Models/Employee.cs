using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProiectTi.Models
{
    public class Employee
    {
        public int? Id { get; set; }
        public string Nume { get; set; }

        public string Prenume { get; set; }

        [Display(Name = "Salar de bază")]
        public double SalarBaza { get; set; }

        public double? Spor { get; set; }

        [Display(Name = "Premii Brute")] public double? PremiiBrute { get; set; }

        [Display(Name = "Brut impozitabil")] public double BrutImpozitabil { get; set; }

        public double Impozit { get; set; }

        public double CAS { get; set; }

        public double CASS { get; set; }

        [Display(Name = "Rețineri")] public double? Retineri { get; set; }

        [Display(Name = "Virat card")]
        public double ViratCard { get; set; }

        [Display(Name = "Poză")]
        public IFormFile SubmitPicture { get; set; }

        [Display(Name = "Poza angajatului")] public DisplayPicture DisplayPicture { get; set; }
    }
}