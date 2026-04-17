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

        Task<(List<EmployeeResponseDto> Data, int TotalCount)> GetEmployeesAsync(
    int page,
    int pageSize,
    string? search,
    string? department);
    }
}