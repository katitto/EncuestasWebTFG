using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Obtener()
        {
            List<Usuario> oListaUsuario = CD_Usuario.ObtenerUsuarios();
            return Json(new { data = oListaUsuario }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Usuario usu)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (usu.IdUsuario == 0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_Usuario.RegistrarUsuario(usu); // GUARDA
            }
            else
            {
                respuesta = CD_Usuario.ModificarUsuario(usu);  //MODIFICA
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(Usuario usu)
        {
            bool respuesta = CD_Usuario.EliminarUsuario(usu);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}