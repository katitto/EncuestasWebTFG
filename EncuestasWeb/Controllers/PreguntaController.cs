using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaModelo;
using CapaDatos;

namespace EncuestasWeb.Controllers
{
    public class PreguntaController : Controller
    {
        // GET: Pregunta
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Obtener()
        {
            List<Pregunta> oListaPregunta = CD_Pregunta.ObtenerPregunta();
            return Json(new { data = oListaPregunta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Pregunta pre)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (pre.IdPregunta == 0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_Pregunta.RegistrarPregunta(pre); // GUARDA
            }
            else
            {
                respuesta = CD_Pregunta.ModificarPregunta(pre);  //MODIFICA
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(Pregunta pre)
        {
            bool respuesta = CD_Pregunta.EliminarPregunta(pre);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}