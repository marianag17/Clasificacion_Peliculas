using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClasificacionPeliculas.Models
{
    public class personal_information
    {
        [Key]
        public int Id { get; set; }
        public int geonameidCity { get; set; }
        public string name { get; set; }
        public DateTime date_of_birth { get; set; }
        public string email { get; set; }
        public int phone_number { get; set; }
        public string address { get; set; }
    }
}
