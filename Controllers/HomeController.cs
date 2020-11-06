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
            return View(db.Notas.ToList());
            
        }

        public IActionResult AgregarNota(string titulo, string cuerpo)
        {
            Nota nuevaNota = new Nota{
                Titulo = titulo,
                Cuerpo = cuerpo
            };

            db.Notas.Add(nuevaNota);
            db.SaveChanges();
            return View() ;
        }

        
    

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View() ;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
