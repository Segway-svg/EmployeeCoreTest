using WebApplication3.Models.Response;
using WebApplication3.Responses;

namespace WebApplication3.Commands.Interfaces
{
    public interface IGetEmployeesByCompanyCommand
    {
        Task<OperationResultResponse<List<EmployeeResponse>>> ExecuteAsync(int companyId);
    }
}
