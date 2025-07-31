using WebApplication3.Requests;
using WebApplication3.Responses;

namespace WebApplication3.Mappers
{
    public interface IDtoEmployeeMapper
    {
        EmployeeResponse Map(Employee editEmployeeRequest);
    }
}
