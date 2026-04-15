using EMS.Domain.Entities;

namespace EMS.Application.Services
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<List<Employee>> GetAllEmployeesAsync();
    }
}