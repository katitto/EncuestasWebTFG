using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Rol
    {
        public int IdRol { get; set; }
        /*meter más controles: en todas las entidades 
         https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validation-with-the-data-annotation-validators-cs
        */
        [Required]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "EL campo debe tener como mínimo 3 caracteres y como máximo 300")] 
        public string Nombre { get; set; }
        /*Metemos controles en aquellos campos que sabemos que no pueden ser nulos*/
        [StringLength(300)]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }




    }
}
