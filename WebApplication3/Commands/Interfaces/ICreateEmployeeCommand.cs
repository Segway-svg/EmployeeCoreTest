using WebApplication3.Responses;

namespace WebApplication3.Commands.Interfaces
{
    public interface ICreateEmployeeCommand
    {
        Task<OperationResultResponse<int>> ExecuteAsync(CreateEmployeeRequest request);
    }
}
