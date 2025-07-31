using WebApplication3.Responses;

namespace WebApplication3.Commands.Interfaces
{
    public interface IDeleteEmployeeCommand
    {
        Task<OperationResultResponse<bool>> ExecuteAsync(int employeeId);
    }
}