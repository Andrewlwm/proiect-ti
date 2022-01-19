using ProiectTi.Services.Abstractions;
using System.Data;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using ProiectTi.Data.Models;
using System.Collections.Generic;
using ProiectTi.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ProiectTi.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _database;
        private readonly IDtoMapper _mapper;
        private readonly ILogger _logger;

        public EmployeeRepository(IDbConnection database, IDtoMapper mapper, ILogger<EmployeeRepository> logger)
        {
            _database = database;
            _mapper = mapper;
            _logger = logger;
        }

        public async void InitTables()
        {
            _database.CreateTableIfNotExists<Percentages>();
            _database.CreateTableIfNotExists<EmployeeDto>();
            if(await _database.SingleByIdAsync<Percentages>(1) is null)
            {
                _ = _database.SaveAsync(new Percentages());
            }
        }

        public Task AddEmployeeAsync(Employee employee)
        {
            return _database.SaveAsync(_mapper.EmployeeToDto(employee));
        }

        public Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            return _database.SingleByIdAsync<EmployeeDto>(id);
        }

        public Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            return _database.SelectAsync<EmployeeDto>();
        }

        public Task<List<EmployeeDto>> GetEmployeesBySearchStringAsync(string search)
        {
            return _database.SelectAsync<EmployeeDto>(e => e.Nume.Contains(search) || e.Prenume.Contains(search));
        }

        public Task UpdateEmployee(Employee employee)
        {
            return _database.UpdateAsync(_mapper.EmployeeToDto(employee));
        }

        public Task<Percentages> GetPercentages()
        {
            return _database.SingleByIdAsync<Percentages>(1);
        }

        public Task UpdatePercentages(Percentages percentages)
        {
            return _database.SaveAsync(percentages);
        }

        public Task DeleteEmployeeById(int id)
        {
            return _database.DeleteByIdAsync<EmployeeDto>(id);
        }
    }
}