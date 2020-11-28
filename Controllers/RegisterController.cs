using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using asap.mvc.Models;

namespace asap.mvc.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;

        private readonly NotasContext db;

        public RegisterController(ILogger<RegisterController> logger, NotasContext context)
        {
            _logger = logger;
            db = context;
        }

        public  IActionResult Register()
        {
            return View() ;
        }

        [HttpPost]
        public IActionResult Register(string email, string nombre)
        {
            Usuario nuevoUsuario = new Usuario
            {
                Mail = email,
                Nombre = nombre
            };

            db.Usuarios.Add(nuevoUsuario);
            db.SaveChanges();
            HttpContext.Session.Set<Usuario>("UsuarioLogueado", nuevoUsuario);
            return Redirect("/Home/Index");

        }
    }
}