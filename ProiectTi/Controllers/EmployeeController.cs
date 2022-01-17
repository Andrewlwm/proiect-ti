using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProiectTi.Data.Models;
using ProiectTi.Services.Abstractions;
using System.Data;
using System.IO;
using ServiceStack.OrmLite;
using System.Threading.Tasks;
using ProiectTi.Models;
using System;
using System.Collections.Generic;

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
            return RedirectToAction("Create", "Employee");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            await _employeeRepository.AddEmployeeAsync(model);
            return RedirectToAction("Create");
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
            return View(employess as IEnumerable<Employee>);
        }
    }
}