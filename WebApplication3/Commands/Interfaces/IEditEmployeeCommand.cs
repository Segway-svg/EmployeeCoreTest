using WebApplication3.Models.Response;
using WebApplication3.Requests;

namespace WebApplication3.Commands.Interfaces
{
    public interface IEditEmployeeCommand
    {
        Task<OperationResultResponse<Employee>> ExecuteAsync(int id, EditEmployeeRequest request);
    }
}
