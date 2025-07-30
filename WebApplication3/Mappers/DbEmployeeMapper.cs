namespace WebApplication3.Mappers
{
    public class DbEmployeeMapper : IDbEmployeeMapper
    {
        public Employee Map(CreateEmployeeRequest createEmployeeRequest)
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
