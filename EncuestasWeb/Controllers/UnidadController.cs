using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaDatos;
using CapaModelo;

namespace EncuestasWeb.Controllers
{
    public class UnidadController : Controller
    {
        // GET: Unidad
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Obtener()
        {
            List<Unidad> oListaUnidad = CD_Unidad.ObtenerUnidad();
            return Json(new { data = oListaUnidad }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Unidad uni)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (uni.IdUnidad == 0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_Unidad.RegistrarUnidad(uni); // GUARDA
            }
            else
            {
                respuesta = CD_Unidad.ModificarUnidad(uni);  //MODIFICA
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(Unidad uni)
        {
            bool respuesta = CD_Unidad.EliminarUnidad(uni);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}