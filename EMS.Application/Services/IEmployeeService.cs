using EMS.Application.DTOs;

namespace EMS.Application.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDto> AddEmployeeAsync(CreateEmployeeDto dto);
        Task<List<EmployeeResponseDto>> GetAllEmployeesAsync();
    }
}