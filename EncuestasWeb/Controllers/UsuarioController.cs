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
            List<Usuario> oListaUsuarios = CD_Usuario.ObtenerUsuarios();
            return Json(new { data = oListaUsuarios }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Guardar(Usuario objeto)
        {
            bool respuesta = false; // la respuesta que devuelve nuestro procedimiento

            if (objeto.IdUsuario == 0) //si el objeto que me pasan tiene el id = 0, es decir, no existe entonces la clave que nos trae la encriptanos, en nuestro caso no aplica
            {

                respuesta = CD_Usuario.RegistrarUsuario(objeto); // GUARDA
            }
            else
            {
                respuesta = CD_Usuario.ModificarUsuario(objeto);  //MODIFICA
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Usuario.EliminarUsuario(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}