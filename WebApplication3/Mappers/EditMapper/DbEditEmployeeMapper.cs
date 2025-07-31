using WebApplication3.Requests;

namespace WebApplication3.Mappers.EditMapper
{
    public class DbEditEmployeeMapper : IDbEditEmployeeMapper
    {
        public Employee Map(EditEmployeeRequest editEmployeeRequest)
        {
            return new Employee()
            {
                Name = editEmployeeRequest.Name,
                Surname = editEmployeeRequest.Surname,
                Phone = editEmployeeRequest.Phone,
                CompanyId = (int)editEmployeeRequest.CompanyId,
                Passport = editEmployeeRequest.Passport,
                Department = editEmployeeRequest.Department,
            };
        }
    }
}
