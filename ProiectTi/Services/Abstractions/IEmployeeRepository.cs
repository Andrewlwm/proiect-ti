using Microsoft.AspNetCore.Http;
using ProiectTi.Data.Models;
using ProiectTi.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProiectTi.Services.Abstractions
{
    public interface IEmployeeRepository
    {
        void InitTables();
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<List<EmployeeDto>> GetEmployeesBySearchStringAsync(string search);
        Task<Percentages> GetPercentages();
        Task UpdatePercentages(Percentages percentages);
        Task DeleteEmployeeById(int id);
    }
}