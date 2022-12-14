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
            return Json(oListaEjePrincipal, JsonRequestBehavior.AllowGet);
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
        /* NO LO HE USADO
        public JsonResult ObtenerHijos(int id = 0)
        {

            List<EjePrincipal> oListaEjePrincipal = CD_EjePrincipal.ObtenerHijosEjePrincipal(id);
            return Json(new { data = oListaEjePrincipal }, JsonRequestBehavior.AllowGet);
        }*/

       /* public JsonResult ObtenerPerfilById(int id)
        {
            List<Perfil> oListaPerfil= CD_EjePrincipal.ObtenerPerfilById(id);
            return Json(oListaPerfil, JsonRequestBehavior.AllowGet);
        }*/

        public JsonResult ObtenerPerfiles()
        {
            List<Perfil> rptListaPerfil = new List<Perfil>();
            rptListaPerfil = CD_Perfil.ObtenerPerfil();
            return Json(rptListaPerfil, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerGeografias()
        {
            List<Geografia> rptListaGeografia = new List<Geografia>();
            rptListaGeografia = CD_Geografia.ObtenerGeografia();
            return Json(rptListaGeografia, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerPadres(int id= 0)
        {
            List<EjePrincipal> rptListaEjePrincipal = new List<EjePrincipal>();
            rptListaEjePrincipal = CD_EjePrincipal.ObtenerEjePrincipal().Where(x => x.IdEjePadre == id).ToList();
            return Json(rptListaEjePrincipal, JsonRequestBehavior.AllowGet);
        }
        //obtener Encuestas
        public JsonResult ObtenerEncuestas(int id)
        {
            List<Data> rptListaEncuestas = CD_Data.ObtenerEncuestas(id);

            return Json (rptListaEncuestas, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerEncuestasTabla(int id)
        {
            List<Data> rptListaEncuestas = CD_Data.ObtenerEncuestas(id);

            return Json(rptListaEncuestas , JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public JsonResult GuardarRespuesta(Data objeto)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            respuesta = CD_Data.ActualizarRespuesta(objeto);  //MODIFICA
           
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}
