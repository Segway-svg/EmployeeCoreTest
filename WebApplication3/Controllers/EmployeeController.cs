using Microsoft.AspNetCore.Mvc;
using WebApplication3.Commands.Interfaces;
using WebApplication3.Models.Response;
using WebApplication3.Repositories;
using WebApplication3.Requests;
using WebApplication3.Responses;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost("Create")]
        public async Task<OperationResultResponse<int>> AddEmployee(
            [FromBody] CreateEmployeeRequest employee,
            [FromServices] ICreateEmployeeCommand createEmployeeCommand)
        {
             return await createEmployeeCommand.ExecuteAsync(employee);
        }

        [HttpDelete("Delete")]
        public async Task<OperationResultResponse<bool>> DeleteEmployee(
            [FromBody] int id,
            [FromServices] IDeleteEmployeeCommand deleteEmployeeCommand)
        {
            return await deleteEmployeeCommand.ExecuteAsync(id);
        }

        [HttpGet("GetByCompany/{companyId}")]
        public async Task<ActionResult<OperationResultResponse<List<EmployeeResponse>>>> GetEmployeesByCompany(
            int companyId,
            [FromServices] IGetEmployeesByCompanyCommand getEmployeesByCompanyCommand)
        {
            var employees = await getEmployeesByCompanyCommand.ExecuteAsync(companyId);

            return employees;
        }

        [HttpGet("GetByDepartment/{departmentName}")]
        public async Task<IEnumerable<Employee>> GetEmployeesByCompany(string departmentName)
        {
            var employees = await _employeeRepository.GetEmployeesByDepartmentAsync(departmentName);

            return employees;
        }

        [HttpPut("Update/{id}")]
        public async Task<OperationResultResponse<EmployeeResponse>> GetEmployeesByCompany(
            int id, 
            [FromBody] EditEmployeeRequest employee,
            [FromServices] IEditEmployeeCommand editEmployeeCommand)
        {
            var updatedEmployee = await editEmployeeCommand.ExecuteAsync(id, employee);

            return updatedEmployee;
        }
    }
}