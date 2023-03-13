using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClasificacionPeliculas.Models
{
    public class region
    {
        [Key]
        public int geonameid {  get; set; }
       public int geonameidCountry { get; set; }
       public string? name { get; set; } = null!;
       public string? name2 { get; set; } = null!;
       public decimal longitude { get; set; }
       public decimal latitude { get; set; }
    }
}
