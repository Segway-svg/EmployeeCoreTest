using WebApplication3.Responses;

namespace WebApplication3.Mappers
{
    public class DtoEmployeeMapper : IDtoEmployeeMapper
    {
        public EmployeeResponse Map(Employee employee)
        {
            return employee is null
            ? null
            : new EmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Phone = employee.Phone,
                CompanyId = employee.CompanyId,
                Department = employee.Department,
                Passport = employee.Passport,
            };
        }
    }
}
