using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Obtener()

        {
            List<Rol> oListaRol = CD_Rol.ObtenerRoles();
            return Json(new { data = oListaRol }, JsonRequestBehavior.AllowGet);
        }


    }
}