using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SalesWebMvc2.Models;
using SalesWebMvc2.Models.ViewModels;
using SalesWebMvc2.Services;
using SalesWebMvc2.Services.Exceptions;

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
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departments = departments };
                return View(viewModel);
            }

            _vendedorService.Insert(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided"});
            }

            var obj = _vendedorService.FindById(id.Value);
            if (id == null)
            {
                return  RedirectToAction(nameof(Error), new { message = "Id Not Found"});
            }

            return View(obj);
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Delete(int id)
            {
                _vendedorService.Remove(id);
                return RedirectToAction(nameof(Index));
            }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _vendedorService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }
            return View(obj);
        }
         public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found"});
            }

            var obj = _vendedorService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found"});
            }
            List<Department> departments = _departmentService.FindAll();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departments = departments };
            return View(viewModel);
         }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departments = departments };
                return View(viewModel);
            }

                if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Mismatch"});
            }           
            try
            {
                _vendedorService.Update(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e. Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }        
                  
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            };

            return View(viewModel);
        }
    }
}
