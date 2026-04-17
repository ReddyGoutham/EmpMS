using EMS.Application.DTOs;

namespace EMS.Application.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDto> AddEmployeeAsync(CreateEmployeeDto dto);
        Task<List<EmployeeResponseDto>> GetAllEmployeesAsync();

        Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);

        Task<bool> DeleteEmployeeAsync(int id);

        Task<EmployeeResponseDto> GetEmployeeByIdAsync(int id);

        Task<List<EmployeeResponseDto>> GetEmployeesAsync(
     int page,
     int pageSize,
     string? search,
     string? department);

        //Task<List<EmployeeResponseDto>> GetEmployeesAsync(int page, int pageSize, string? search, string? department);
    }
}