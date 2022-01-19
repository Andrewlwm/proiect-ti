using ServiceStack.DataAnnotations;

namespace ProiectTi.Data.Models
{
    public class Percentages
    {
        [PrimaryKey] public int Id { get; set; } = 1;
        [Required] public double CAS { get; set; } = 0.25;
        [Required] public double CASS { get; set; } = 0.1;
        [Required] public double Impozit { get; set; } = 0.1;
    }
}