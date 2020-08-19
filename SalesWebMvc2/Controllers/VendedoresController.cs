using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc2.Models;
using SalesWebMvc2.Models.ViewModels;
using SalesWebMvc2.Services;
 

namespace SalesWebMvc2.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartmentService _departmentService;
        // Injeção de depencencia
        public VendedoresController (VendedorService vendedorService, DepartmentService departmentService)
        {
            _vendedorService = vendedorService;
            _departmentService = departmentService;
        }            

        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();
            return View(list);
        }
         public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new VendedorFormViewModel { Departments = departments };
            return View(viewModel);
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
