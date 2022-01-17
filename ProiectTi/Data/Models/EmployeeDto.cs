using ServiceStack.DataAnnotations;

namespace ProiectTi.Data.Models
{
    [Alias("Employees")]
    public class EmployeeDto
    {
        [PrimaryKey] [AutoIncrement] public int Id { get; set; }
        public string Nume { get; set; }

        public string Prenume { get; set; }

        public double SalarBaza { get; set; }

        public double Spor { get; set; }

        public double PremiiBrute { get; set; }

        public double BrutImpozitabil { get; set; }

        public double Impozit { get; set; }

        public double CAS { get; set; }

        public double CASS { get; set; }

        public double Retineri { get; set; }

        public double ViratCard { get; set; }

        public EmployeePicture Picture { get; set; }
    }
}