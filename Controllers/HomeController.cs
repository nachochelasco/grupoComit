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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly NotasContext db;

        public HomeController(ILogger<HomeController> logger, NotasContext context)
        {
            _logger = logger;
            db = context;
        }

        public  IActionResult Index()
        {
            Usuario user = HttpContext.Session.Get<Usuario>("UsuarioLogueado");
            if ( user != null) {
                return View(db.Notas.ToList());
            }
            else {
                return Redirect("/Home/Login");
            }
        }

        public IActionResult AgregarNota(string titulo, string cuerpo) 
        {
            Usuario user = HttpContext.Session.Get<Usuario>("UsuarioLogueado");
            if ( user != null) {

                Usuario usuarioBase = db.Usuarios.FirstOrDefault(u => u.Mail.Equals(user.Mail));
                Nota nuevaNota = new Nota{
                Titulo = titulo,
                Cuerpo = cuerpo,
                Creador = usuarioBase
                };

                db.Notas.Add(nuevaNota);
                db.SaveChanges();
                return Redirect("/Home/Index");
            }
            else {
                return Json("No se puede agregar una nota si no estas logueado");
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View() ;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string nombre)
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
