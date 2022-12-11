using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class EncuestaController : Controller
    {
        private static Usuario SesionUsuario;
        // GET: Encuesta
        public ActionResult Index()
        {
            SesionUsuario = (Usuario)Session["Usuario"];

            return View();
        }

        public JsonResult Obtener()
        {
            List<Encuesta> olista = CD_Encuesta.Obtener();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Guardar(Encuesta objeto)
        {
            bool respuesta = false;

            if (objeto.IdEncuesta == 0)
            {
                objeto.oUsuario = SesionUsuario;

                respuesta = CD_Encuesta.Registrar(objeto);
            }
            else
            {
                respuesta = CD_Encuesta.Modificar(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
}