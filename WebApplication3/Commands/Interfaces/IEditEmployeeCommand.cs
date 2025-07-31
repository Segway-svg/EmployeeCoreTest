using WebApplication3.Models.Response;
using WebApplication3.Requests;
using WebApplication3.Responses;

namespace WebApplication3.Commands.Interfaces
{
    public interface IEditEmployeeCommand
    {
        Task<OperationResultResponse<EmployeeResponse>> ExecuteAsync(int id, EditEmployeeRequest request);
    }
}
