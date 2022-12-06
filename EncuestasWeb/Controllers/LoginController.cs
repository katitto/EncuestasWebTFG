using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario ObjUsuario = CD_Usuario.ObtenerUsuarios().Where(x => x.Email == correo && x.Contrasena == clave).FirstOrDefault();

            if (ObjUsuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no correcta";
                return View();
            }

            Session["Usuario"] = ObjUsuario;

            return RedirectToAction("Index", "Home");
        }
    }
}