using CapaDatos;
using CapaModelo;
using EncuestasWeb.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class IndEncController : Controller
    {
        // GET: IndEnc
        private static Usuario SesionUsuario;
        // GET: Candidato
        public ActionResult Index()
        {
            SesionUsuario = (Usuario)Session["Usuario"];
            return View();
        }

        //Obtener Encuesta --perfect
        public JsonResult Obtener()
        {
            List<Encuesta> olista = CD_Encuesta.Obtener();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        //obtener preguntas en el combo para agregarlas
        [HttpGet]
        public JsonResult ObtenerPreguntas()
        {
            List<Indicador> olista = CD_Indicador.ObtenerIndicador();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        //obtener preguntas filtrar por encuesta
        [HttpGet]
        public JsonResult ObtenerPreguntasFiltradas(int idencuesta)
        {
            List<IndEnc> olista = CD_IndEnc.ObtenerIndEnc().Where(x => x.IdEncuesta == idencuesta).ToList();

            olista = (from row in olista
                      select new IndEnc()
                      {
                          IdIndicador = row.IdIndicador,
                          IdEncuesta = row.IdEncuesta,
                          Descripcion = row.Descripcion,
                          IdUnidad = row.IdUnidad,
                          IdTipo = row.IdTipo,
                          IdPerfil = row.IdPerfil,
                          RefIndicador = row.RefIndicador,
                          Tipo = row.Tipo,
                          Nombre = row.Nombre,
                          RefPerfil = row.RefPerfil
                      }).ToList();

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }
        //obtener datos despliegue encuesta filtrar por encuesta
        [HttpGet]
        public JsonResult ObtenerDatosDesplegarEncuesta(int idencuesta)
        {
            List<IndEnc> olista = CD_IndEnc.ObtenerDatosDesplegarEncuesta(idencuesta);
            return Json(olista, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Guardar(IndEnc objeto)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (objeto.IdIndicador != 0 && objeto.IdEncuesta !=0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_IndEnc.RegistrarIndEnc(objeto); // GUARDA
            }
         
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Eliminar(int IdIndicador = 0, int IdEncuesta = 0)
        {
            bool respuesta = CD_IndEnc.EliminarIndEnc(IdIndicador, IdEncuesta);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        //insertar datos desplegados por encuesta
        [HttpPost]
        public JsonResult GuardarDesplegarEncuesta(Data objeto)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (objeto.IdIndicador != 0 && objeto.IdEncuesta != 0 && objeto.IdEje != 0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_Data.RegistrarDesplegarEncuesta(objeto); // GUARDA
            }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }



    }
   

}

    