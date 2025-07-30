namespace WebApplication3.Repositories
{
    public interface IEmployeeRepository
    {
        Task<int?> AddEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesByCompanyAsync(int companyId);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string departmentName);
        Task<Employee> UpdateEmployeeAsync(int id, Employee employee);
    }
}