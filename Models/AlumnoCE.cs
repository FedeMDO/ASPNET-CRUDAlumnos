using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class AlumnoCE
    {
        public int ID { get; set; }

        [Required]
        public string APELLIDO { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        [Required] 
        public int EDAD { get; set; }

        [Required]
        public string SEXO { get; set; }

        public DateTime MOMENTO { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public int? CIUDAD_ID { get; set; }

        public string NombreCiudad { get; set; }

    }

    [MetadataType(typeof(AlumnoCE))]
    public partial class ALUMNO
    {
        public string NombreCompleto { get { return NOMBRE + " " + APELLIDO; } }
        public string NombreCiudad { get; set; }
    }
}