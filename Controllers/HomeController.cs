﻿using System;
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
                List<Nota> notasUser = new List<Nota>();
                notasUser = db.Notas.Where(n => n.Creador.Mail.Equals(user.Mail)).ToList() ;
                return View("Index",notasUser);
            }
            else {
                return Redirect("/Login/Account");
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

        public JsonResult AgregarUsuarioALaSession(string email, string nombre)
        {
            Usuario nuevoUsuario = new Usuario{
                Mail = email, 
                Nombre = nombre
            };

            HttpContext.Session.Set<Usuario>("UsuarioLogueado", nuevoUsuario);
            return Json(nuevoUsuario);
        }

        public IActionResult SacarUsuarioEnSesion()
        {
            HttpContext.Session.Remove("UsuarioLogueado");
            return RedirectToAction("Account", "Login") ;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
