using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            return View();
        }
        //OBTIENE LISTA DE PERFILES
        public JsonResult Obtener()

        {
            List<Perfil> oListaPerfil = CD_Perfil.ObtenerPerfil();
            return Json(new { data = oListaPerfil }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Guardar(Perfil objeto)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (objeto.IdPerfil == 0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_Perfil.RegistrarPerfil(objeto); // GUARDA
            }
            else
            {
                respuesta = CD_Perfil.ModificarPerfil(objeto);  //MODIFICA
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Perfil.EliminarPerfil(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
}