using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Views
{
    public class EjePrincipalController : Controller
    {
        // GET: EjePrincipal
        public ActionResult Index()
        {
            return View();
        }
        //OBTIENE LISTA DE EjePrincipal
        public JsonResult Obtener()

        {
            List<EjePrincipal> oListaEjePrincipal = CD_EjePrincipal.ObtenerEjePrincipal();
            return Json(new { data = oListaEjePrincipal }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Guardar(EjePrincipal objeto)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (objeto.IdEje == 0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_EjePrincipal.RegistrarEjePrincipal(objeto); // GUARDA
            }
            else
            {
                respuesta = CD_EjePrincipal.ModificarEjePrincipal(objeto);  //MODIFICA
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_EjePrincipal.EliminarEjePrincipal(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerHijos(int id = 0)
        {

            List<EjePrincipal> oListaEjePrincipal = CD_EjePrincipal.ObtenerHijosEjePrincipal(id);
            return Json(new { data = oListaEjePrincipal }, JsonRequestBehavior.AllowGet);
        }

    }
}
