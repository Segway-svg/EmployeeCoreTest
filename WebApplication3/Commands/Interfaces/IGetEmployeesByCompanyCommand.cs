using WebApplication3.Responses;

namespace WebApplication3.Commands.Interfaces
{
    public interface IGetEmployeesByCompanyCommand
    {
        Task<OperationResultResponse<IEnumerable<EmployeeResponse>>> ExecuteAsync(int companyId);
    }
}
