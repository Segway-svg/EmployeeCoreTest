using WebApplication3.Models.Response;

namespace WebApplication3.Commands
{
    public interface ICreateEmployeeCommand
    {
        Task<OperationResultResponse<int>> ExecuteAsync(CreateEmployeeRequest request);
    }
}
