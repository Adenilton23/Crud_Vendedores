using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc2.Services;

namespace SalesWebMvc2.Controllers
{
    public class RegistroVendasController : Controller
    {
        private readonly RegistroVendaService _registroVendaService;

        public RegistroVendasController(RegistroVendaService registroVendaService)
        {
            _registroVendaService = registroVendaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async  Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _registroVendaService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
