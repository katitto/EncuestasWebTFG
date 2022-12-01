using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class GeografiaController : Controller
    {
        // GET: Geografia
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TablaJSK()
        {
            return View();
        }
        //OBTIENE LISTA DE Geografia
        public JsonResult Obtener()

        {
            List<Geografia> oListaGeografia = CD_Geografia.ObtenerGeografia();
            return Json(new { data = oListaGeografia }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Guardar(Geografia objeto)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (objeto.IdGeografia == 0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_Geografia.RegistrarGeografia(objeto); // GUARDA
            }
            else
            {
                respuesta = CD_Geografia.ModificarGeografia(objeto);  //MODIFICA
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Geografia.EliminarGeografia(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerHijos(int id = 0)
        {
            List<Geografia> oListaGeografia = CD_Geografia.ObtenerHijosGeografia(id);
            return Json(oListaGeografia, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerTablaNested()
        {
            List<Geografia> oListaGeografia = CD_Geografia.ObtenerGeografia();
            return Json(oListaGeografia , JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerBusqueda(string pais)
        {
            List<Geografia> oListaGeografia = CD_Geografia.ObtenerBusquedaGeografia(pais);
            return Json(oListaGeografia , JsonRequestBehavior.AllowGet);
        }

    }
}
