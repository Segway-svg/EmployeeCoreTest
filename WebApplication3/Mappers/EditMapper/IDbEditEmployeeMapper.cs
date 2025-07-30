using WebApplication3.Requests;

namespace WebApplication3.Mappers.EditMapper
{
    public interface IDbEditEmployeeMapper
    {
        Employee Map(EditEmployeeRequest editEmployeeRequest);
    }
}
