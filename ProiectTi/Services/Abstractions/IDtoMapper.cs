using ProiectTi.Data.Models;
using ProiectTi.Models;

namespace ProiectTi.Services.Abstractions
{
    public interface IDtoMapper
    {
        Employee DtoToEmployee(EmployeeDto dto);
        EmployeeDto EmployeeToDto(Employee employee);
    }
}