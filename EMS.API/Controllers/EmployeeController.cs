using EMS.Application.DTOs;
using EMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous] // must be logged in
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add(CreateEmployeeDto dto)
    {
        var result = await _service.AddEmployeeAsync(dto);
        return Ok(result);
    }

    //[HttpGet]
    //[Authorize(Roles = "Admin,User")]
    //public async Task<IActionResult> GetAll()
    //{
    //    var result = await _service.GetAllEmployeesAsync();
    //    return Ok(result);
    //}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto dto)
    {
        var result = await _service.UpdateEmployeeAsync(id, dto);

        if (!result)
            return NotFound(new { message = "Employee not found" });

        return Ok(new { message = "Employee updated successfully" });
    }

    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception("This is a test exception");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var result = await _service.DeleteEmployeeAsync(id);

        if (!result)
            return NotFound(new { message = "Employee not found" });

        return Ok(new { message = "Employee deleted successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _service.GetEmployeeByIdAsync(id);

        if (employee == null)
            return NotFound(new { message = "Employee not found" });

        return Ok(employee);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
      int page = 1,
      int pageSize = 10,
      string? search = null,
      string? department = null)
    {
        var result = await _service.GetEmployeesAsync(page, pageSize, search, department);
        return Ok(result);
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAll(
    //[FromQuery] int page = 1,
    //[FromQuery] int pageSize = 10,
    //[FromQuery] string? search = null,
    //[FromQuery] string? department = null)
    //{
    //    var result = await _service.GetEmployeesAsync(page, pageSize, search, department);
    //    return Ok(result);
    //}
}