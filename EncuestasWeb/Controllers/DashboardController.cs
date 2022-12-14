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
        public JsonResult ObtenerResultados(int idencuesta)
        {
            List<Data> olista = CD_Data.ObtenerResultados(idencuesta);

            olista = (from row in olista
                      select new Data()
                      {
                          Total = row.Total,
                          IdIndicador = row.IdIndicador

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