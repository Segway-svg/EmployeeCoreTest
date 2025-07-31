using Azure.Core;
using WebApplication3.Requests;
using WebApplication3.Responses;

namespace WebApplication3.Mappers
{
    public class DtoEmployeeMapper : IDtoEmployeeMapper
    {
        public EmployeeResponse Map(Employee editEmployeeRequest)
        {
            return editEmployeeRequest is null
            ? null
            : new EmployeeResponse
            {
                Name = editEmployeeRequest.Name,
                Surname = editEmployeeRequest.Surname,
                Phone = editEmployeeRequest.Phone,
                CompanyId = editEmployeeRequest.CompanyId,
                Department = editEmployeeRequest.Department,
                Passport = editEmployeeRequest.Passport,
            };
        }
    }
}
