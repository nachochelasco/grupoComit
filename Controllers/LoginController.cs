using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using asap.mvc.Models;
using asap.mvc.Controllers;

namespace asap.mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        private readonly NotasContext db;

        public LoginController(ILogger<LoginController> logger, NotasContext context)
        {
            _logger = logger;
            db = context;
        }

        public JsonResult AgregarUsuarioALaSession(string email, string nombre)
        {
            Usuario nuevoUsuario = new Usuario{
                Mail = email, 
                Nombre = nombre
            };

            HttpContext.Session.Set<Usuario>("UsuarioLogueado", nuevoUsuario);
            return Json(nuevoUsuario);
        }

        public IActionResult Account() {
            return View() ;
        }

        [HttpPost]
        public  IActionResult Account(string email, string nombre) {
            {
            Usuario userCheck = db.Usuarios.FirstOrDefault(u => u.Mail == email);
            if(userCheck != null) 
            {
                if(userCheck.Nombre == nombre) {
                    AgregarUsuarioALaSession(email, nombre) ;
                    return RedirectToAction("Index", "Home");
                }
                else {
                    return Redirect("/Login/Account");
                }
            } else {
                return Redirect("/Login/Account");
             }
            }
        }
    }
}
            