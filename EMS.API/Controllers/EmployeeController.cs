using EMS.Application.DTOs;
using EMS.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateEmployeeDto dto)
    {
        var result = await _service.AddEmployeeAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllEmployeesAsync();
        return Ok(result);
    }
}