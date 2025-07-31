using WebApplication3.Responses;

namespace WebApplication3.Commands.Interfaces
{
    public interface IGetEmployeesByDepartmentCommand
    {
        Task<OperationResultResponse<IEnumerable<EmployeeResponse>>> ExecuteAsync(string departmentName);
    }
}
