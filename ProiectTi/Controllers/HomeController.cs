using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectTi.Data.Models;
using ProiectTi.Models;
using ProiectTi.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectTi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(ILogger<HomeController> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            employeeRepository.InitTables();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Employee");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}