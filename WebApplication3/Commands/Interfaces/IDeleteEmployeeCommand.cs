using WebApplication3.Models.Response;

namespace WebApplication3.Commands.Interfaces
{
    public interface IDeleteEmployeeCommand
    {
        Task<OperationResultResponse<bool>> ExecuteAsync(int employeeId);
    }
}