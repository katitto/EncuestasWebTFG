using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EncuestasWeb.Controllers
{
    public class InformeMapsController : Controller
    {
        // GET: InformeMaps
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Obtener()

        {
            List<EjePrincipal> oListaEjePrincipal = CD_EjePrincipal.ObtenerMapa();
            return Json(oListaEjePrincipal, JsonRequestBehavior.AllowGet);
        }
    }
}