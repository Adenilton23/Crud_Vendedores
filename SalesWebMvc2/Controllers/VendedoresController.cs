using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc2.Models;
using SalesWebMvc2.Services;

namespace SalesWebMvc2.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        // Injeção de depencencia
        public VendedoresController (VendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }            

        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();
            return View(list);
        }
         public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            _vendedorService.Insert(vendedor);
            return RedirectToAction(nameof(Index));
        }
    }
}
