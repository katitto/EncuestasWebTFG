using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class InformeUsuariosController : Controller
    {
        // GET: InformeUsuarios
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Obtener()

        {
            List<Usuario> oListaUsuarios = CD_Usuario.ObtenerUsuarios();
            return Json(new { data = oListaUsuarios }, JsonRequestBehavior.AllowGet);
        }
    }


}