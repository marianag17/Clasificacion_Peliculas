using NuGet.Protocol.Plugins;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClasificacionPeliculas.Models
{
    public class city
    {
        [Key]
        public int  geonameid { get; set; }
        public int geonameidRegion { get; set; }
        public string name { get; set; } 
        public decimal? longitude { get; set; }
        public decimal? latitude { get; set; }
        public int? population { get; set; }
        public string userlastmodified { get; set; }
        public DateTime timecreated { get; set; }
        public DateTime? timemodified { get; set; }
    }
}
