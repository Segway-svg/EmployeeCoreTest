using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplication3.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SqlConnection _connection;

        public EmployeeRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int?> AddEmployeeAsync(Employee employee)
        {   
            var sql = @"
            INSERT INTO Employees (
                    Name, 
                    Surname, 
                    Phone, 
                    CompanyId, 
                    PassportType, 
                    PassportNumber, 
                    DepartmentName, 
                    DepartmentPhone
            )
            VALUES (
                    @Name, 
                    @Surname, 
                    @Phone, 
                    @CompanyId, 
                    @PassportType, 
                    @PassportNumber, 
                    @DepartmentName, 
                    @DepartmentPhone
            )";

            await EnsureConnectionOpenAsync();

            var addedRow = await _connection.ExecuteScalarAsync<int>(sql, new
            {
                employee.Name,
                employee.Surname,
                employee.Phone,
                employee.CompanyId,
                PassportType = employee.Passport?.Type,
                PassportNumber = employee.Passport?.Number,
                DepartmentName = employee.Department?.Name,
                DepartmentPhone = employee.Department?.Phone
            });

            if (addedRow > 0) 
            { 
                var employeeId = await _connection.ExecuteScalarAsync<int>("SELECT TOP 1 * FROM Employees ORDER BY Id DESC");

                return employeeId;
            }

            return null;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var sql = "DELETE FROM Employees WHERE Id = @Id";

            await EnsureConnectionOpenAsync();
            
            var affectedRows = await _connection.ExecuteAsync(sql, new { Id = id });
            
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyAsync(int companyId)
        {
            var sql = @"
            SELECT 
                Id, 
                Name, 
                Surname, 
                Phone, 
                CompanyId,
                PassportType AS [Passport.Type], 
                PassportNumber AS [Passport.Number],
                DepartmentName AS [Department.Name],
                DepartmentPhone AS [Department.Phone]
            FROM 
                Employees
            WHERE 
                CompanyId = @CompanyId";

            await EnsureConnectionOpenAsync();

            return await _connection.QueryAsync<Employee>(sql, new { CompanyId = companyId });
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string departmentName)
        {
            var sql = @"
            SELECT 
                Id, 
                Name, 
                Surname, 
                Phone, 
                CompanyId,
                PassportType AS [Passport.Type], 
                PassportNumber AS [Passport.Number],
                DepartmentName AS [Department.Name],
                DepartmentPhone AS [Department.Phone]
            FROM 
                Employees
            WHERE 
                DepartmentName = @DepartmentName";

            await EnsureConnectionOpenAsync();

            return await _connection.QueryAsync<Employee>(sql, new
            {
                DepartmentName = departmentName
            });
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, Employee employee)
        {
            var sql = @"
            UPDATE 
                Employees
            SET 
                Name = ISNULL(@Name, Name),
                Surname = ISNULL(@Surname, Surname),
                Phone = ISNULL(@Phone, Phone),
                CompanyId = ISNULL(@CompanyId, CompanyId),
                PassportType = ISNULL(@PassportType, PassportType),
                PassportNumber = ISNULL(@PassportNumber, PassportNumber),
                DepartmentName = ISNULL(@DepartmentName, DepartmentName),
                DepartmentPhone = ISNULL(@DepartmentPhone, DepartmentPhone)
            WHERE 
                Id = @Id";

            await EnsureConnectionOpenAsync();

            var affectedRows = await _connection.ExecuteAsync(sql, new
            {
                Id = id,
                employee.Name,
                employee.Surname,
                employee.Phone,
                employee.CompanyId,
                PassportType = employee.Passport?.Type,
                PassportNumber = employee.Passport?.Number,
                DepartmentName = employee.Department?.Name,
                DepartmentPhone = employee.Department?.Phone
            });

            if (affectedRows > 0)
                return employee;
            else
                return null;
        }

        private async Task EnsureConnectionOpenAsync()
        {
            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();
        }
    }
}