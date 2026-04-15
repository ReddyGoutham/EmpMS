using EMS.Application.Services;
using EMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllEmployeesAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            var result = await _service.AddEmployeeAsync(employee);
            return Ok(result);
        }
    }
}