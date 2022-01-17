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

namespace ProiectTi.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _database;
        private readonly IDtoMapper _mapper;

        public EmployeeRepository(IDbConnection database, IDtoMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public void InitTables()
        {
            _database.CreateTableIfNotExists<Percentages>();
            _database.CreateTableIfNotExists<EmployeeDto>();
        }

        public Task AddEmployeeAsync(Employee employee)
        {
            return _database.SaveAsync(_mapper.EmployeeToDto(employee));
        }

        public Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            return _database.SingleByIdAsync<EmployeeDto>(id);
        }

        public  Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            return  _database.SelectAsync<EmployeeDto>();
        }

        public  Task<List<EmployeeDto>> GetEmployeesBySearchStringAsync(string search)
        {
            return  _database.SelectAsync<EmployeeDto>(e => e.Nume.Contains(search) || e.Prenume.Contains(search));
        }
    }
}