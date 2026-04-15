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

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            employee.CreatedAt = DateTime.UtcNow;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        //public async Task<List<Employee>> GetAllEmployeesAsync()
        //{
        //    return await _context.Employees.ToListAsync();
        //}

        public async Task<List<Employee>> GetAllEmployeesAsync() {
            return await _context.Employees.AsNoTracking().ToListAsync();
        }
    }
}