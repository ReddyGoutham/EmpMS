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

        public async Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return false;

            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Department = dto.Department;
            employee.Salary = dto.Salary;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<EmployeeResponseDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return null;

            return new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department
            };
        }

        //    public async Task<List<EmployeeResponseDto>> GetEmployeesAsync(
        //int page,
        //int pageSize,
        //string? search,
        //string? department)
        //    {
        //        var query = _context.Employees.AsQueryable();

        //        if (!string.IsNullOrEmpty(search))
        //        {
        //            query = query.Where(e => e.Name.Contains(search));
        //        }

        //        if (!string.IsNullOrEmpty(department))
        //        {
        //            query = query.Where(e => e.Department == department);
        //        }

        //        var employees = await query
        //            .Skip((page - 1) * pageSize)
        //            .Take(pageSize)
        //            .AsNoTracking()
        //            .Select(e => new EmployeeResponseDto
        //            {
        //                Id = e.Id,
        //                Name = e.Name,
        //                Email = e.Email,
        //                Department = e.Department
        //            })
        //            .ToListAsync();

        //        return employees;
        //    }


        public async Task<(List<EmployeeResponseDto> Data, int TotalCount)> GetEmployeesAsync(
    int page,
    int pageSize,
    string? search,
    string? department)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(e => e.Name.Contains(search));

            if (!string.IsNullOrEmpty(department))
                query = query.Where(e => e.Department == department);

            var totalCount = await query.CountAsync();

            var data = await query
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Department = e.Department
                })
                .ToListAsync();

            return (data, totalCount);
        }
    }
}