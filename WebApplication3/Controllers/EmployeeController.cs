using Microsoft.AspNetCore.Mvc;
using WebApplication3.Commands;
using WebApplication3.Models.Response;
using WebApplication3.Repositories;

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
        public async Task<bool> DeleteEmployee([FromBody] int id)
        {
            var isDeleted = await _employeeRepository.DeleteEmployeeAsync(id);

            return isDeleted;
        }

        [HttpGet("GetByCompany/{companyId}")]
        public async Task<IEnumerable<Employee>> GetEmployeesByCompany(int companyId)
        {
            var employees = await _employeeRepository.GetEmployeesByCompanyAsync(companyId);

            return employees;
        }

        [HttpGet("GetByDepartment/{departmentName}")]
        public async Task<IEnumerable<Employee>> GetEmployeesByCompany(string departmentName)
        {
            var employees = await _employeeRepository.GetEmployeesByDepartmentAsync(departmentName);

            return employees;
        }

        [HttpPut("Update/{id}")]
        public async Task<Employee> GetEmployeesByCompany(int id, [FromBody] Employee employee)
        {
            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(id, employee);

            return updatedEmployee;
        }
    }
}