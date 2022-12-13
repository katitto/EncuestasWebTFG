using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ObtenerResultados(int ideleccion)
        {
            List<Resultado> olista = CD_Votacion.ObtenerResultados(ideleccion);

            olista = (from row in olista
                      select new Resultado()
                      {
                          //Valor = row.Valor,
                          //NombreIndicador = row.NombreIndicador

                      }).ToList();

            return Json(olista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Salir()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}