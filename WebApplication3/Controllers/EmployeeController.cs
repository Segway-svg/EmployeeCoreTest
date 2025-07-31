using Microsoft.AspNetCore.Mvc;
using WebApplication3.Commands.Interfaces;
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
        public async Task<OperationResultResponse<List<EmployeeResponse>>> GetEmployeesByCompany(
            int companyId,
            [FromServices] IGetEmployeesByCompanyCommand getEmployeesByCompanyCommand)
        {
            return await getEmployeesByCompanyCommand.ExecuteAsync(companyId);
        }

        [HttpGet("GetByDepartment/{departmentName}")]
        public async Task<OperationResultResponse<List<EmployeeResponse>>> GetEmployeesByDepartment(
            string departmentName,
            [FromServices] IGetEmployeesByDepartmentCommand getEmployeesByDepartmentCommand)
        {
            return await getEmployeesByDepartmentCommand.ExecuteAsync(departmentName);
        }

        [HttpPut("Update/{id}")]
        public async Task<OperationResultResponse<EmployeeResponse>> EditEmployee(
            int id, 
            [FromBody] EditEmployeeRequest employee,
            [FromServices] IEditEmployeeCommand editEmployeeCommand)
        {
            return await editEmployeeCommand.ExecuteAsync(id, employee);
        }
    }
}