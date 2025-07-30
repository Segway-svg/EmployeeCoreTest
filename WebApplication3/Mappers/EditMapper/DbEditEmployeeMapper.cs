using WebApplication3.Requests;

namespace WebApplication3.Mappers.EditMapper
{
    public class DbEditEmployeeMapper : IDbEditEmployeeMapper
    {
        public Employee Map(EditEmployeeRequest createEmployeeRequest)
        {
            return new Employee()
            {
                Name = createEmployeeRequest.Name,
                Surname = createEmployeeRequest.Surname,
                Phone = createEmployeeRequest.Phone,
                CompanyId = createEmployeeRequest.CompanyId,
                Passport = createEmployeeRequest.Passport,
                Department = createEmployeeRequest.Department,
            };
        }
    }
}
