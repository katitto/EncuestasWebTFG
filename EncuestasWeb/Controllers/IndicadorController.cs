using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class IndicadorController : Controller
    {
        // GET: Indicador
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Guardar(Indicador objeto)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (objeto.IdIndicador == 0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_Indicador.RegistrarIndicador(objeto); // GUARDA
            }
            else
            {
                respuesta = CD_Indicador.ModificarIndicador(objeto);  //MODIFICA
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Indicador.EliminarIndicador(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        //cargar combos
        public JsonResult ObtenerPerfiles()
        {
            List<Perfil> rptListaPerfil = new List<Perfil>();
            rptListaPerfil = CD_Perfil.ObtenerPerfil();
            return Json(rptListaPerfil, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerUnidades()
        {
            List<Unidad> rptListaUnidad = new List<Unidad>();
            rptListaUnidad = CD_Unidad.ObtenerUnidad();
            return Json(rptListaUnidad, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerTipos(int id = 0)
        {
            List<Tipo> rptListaTipo = new List<Tipo>();
            rptListaTipo = CD_Tipo.ObtenerTipo();
            return Json(rptListaTipo, JsonRequestBehavior.AllowGet);
        }

    }
}
