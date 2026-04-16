using EMS.Application.DTOs;
using EMS.Domain.Entities;
using EMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeResponseDto> AddEmployeeAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Department = dto.Department,
                Salary = dto.Salary,
                CreatedAt = DateTime.UtcNow
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department
            };
        }

        public async Task<List<EmployeeResponseDto>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Department = e.Department
                })
                .ToListAsync();
        }
    }
}