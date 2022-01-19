using Microsoft.AspNetCore.Mvc;
using ProiectTi.Data.Models;
using ProiectTi.Models;
using ProiectTi.Services.Abstractions;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectTi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDbConnection _database;
        private readonly IDtoMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IDbConnection database, IDtoMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _database = database;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            AddPercentagesToViewData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            await _employeeRepository.AddEmployeeAsync(model);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            AddPercentagesToViewData();
            return View("../Employee/Create", _mapper.DtoToEmployee(employee));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Employee model, int id)
        {
            await _employeeRepository.UpdateEmployee(model);
            return RedirectToAction("View", new { id });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeRepository.DeleteEmployeeById(id);
            return Ok();
        }

        public async Task<IActionResult> View(int id)
        {
            var dbEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (dbEmployee is null) return View();

            var employee = _mapper.DtoToEmployee(dbEmployee);
            return View(employee);
        }

        public async Task<IActionResult> List()
        {
            var dbEmployees = await _employeeRepository.GetAllEmployeesAsync();
            var employess = Array.ConvertAll(dbEmployees.ToArray(), employeeDto => _mapper.DtoToEmployee(employeeDto));
            return View(employess);
        }

        public async Task<IActionResult> PrintAll()
        {
            var dbEmployees = await _employeeRepository.GetAllEmployeesAsync();
            var employess = Array.ConvertAll(dbEmployees.ToArray(), employeeDto => _mapper.DtoToEmployee(employeeDto));
            ViewData["total"] = employess.Sum(x => x.ViratCard);
            return new ViewAsPdf("_EmployeeTable", employess, ViewData)
            {
                PageSize = Size.A3,
                PageOrientation = Orientation.Landscape,
            };
        }

        public async Task<IActionResult> PrintFluturasi()
        {
            var dbEmployees = await _employeeRepository.GetAllEmployeesAsync();
            var employess = Array.ConvertAll(dbEmployees.ToArray(), employeeDto => _mapper.DtoToEmployee(employeeDto));
            return new ViewAsPdf("_PrintFluturasi", employess)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
            };
        }

        public async Task<IActionResult> Percentages()
        {
            var percentages = await _employeeRepository.GetPercentages();
            return View(percentages);
        }

        [HttpPost]
        public async Task<IActionResult> Percentages(Percentages percentages)
        {
            await _employeeRepository.UpdatePercentages(percentages);
            return RedirectToAction("Index");
        }

        private async void AddPercentagesToViewData()
        {
            var percentages = await _employeeRepository.GetPercentages();
            ViewData["CAS"] = percentages.CAS;
            ViewData["CASS"] = percentages.CASS;
            ViewData["Impozit"] = percentages.Impozit;
        }
    }
}