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
            var departmentId = await _connection.ExecuteScalarAsync<int?>(
                "SELECT Id FROM DepartmentsDb WHERE Name = @Name AND Phone = @Phone",
                new
                {
                    Name = employee.Department?.Name,
                    Phone = employee.Department?.Phone
                });

            if (departmentId == null && employee.Department != null)
            {
                departmentId = await _connection.ExecuteScalarAsync<int>(
                    @"INSERT INTO DepartmentsDb (Name, Phone) 
                    VALUES (@Name, @Phone);
                    SELECT CAST(SCOPE_IDENTITY() as int)",
                    new
                    {
                        Name = employee.Department.Name,
                        Phone = employee.Department.Phone
                    });
            }

            if (departmentId == null)
            {
                throw new ArgumentException("Нет соответствующего департамента");
            }

            var sql = @"
                INSERT INTO EmployeesDb (
                        Name, 
                        Surname, 
                        Phone,
                        CompanyId, 
                        DepartmentId,
                        PassportType, 
                        PassportNumber
                )
                VALUES (
                        @Name, 
                        @Surname, 
                        @Phone, 
                        @CompanyId, 
                        @DepartmentId,
                        @PassportType, 
                        @PassportNumber
                );
                SELECT CAST(SCOPE_IDENTITY() as int)";

            await EnsureConnectionOpenAsync();

            var employeeId = await _connection.ExecuteScalarAsync<int>(sql, new
            {
                employee.Name,
                employee.Surname,
                employee.Phone,
                employee.CompanyId,
                DepartmentId = departmentId,
                PassportType = employee.Passport?.Type,
                PassportNumber = employee.Passport?.Number
            });

            return employeeId;
        }


        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var sql = "DELETE FROM EmployeesDb WHERE Id = @Id";

            await EnsureConnectionOpenAsync();

            var affectedRows = await _connection.ExecuteAsync(sql, new { Id = id });

            return affectedRows > 0;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyAsync(int companyId)
        {
            var sql = @"
                SELECT 
                    e.Id, 
                    e.Name, 
                    e.Surname, 
                    e.Phone, 
                    e.CompanyId,
                    e.PassportType AS [Passport.Type], 
                    e.PassportNumber AS [Passport.Number],
                    d.Name AS [Department.Name],
                    d.Phone AS [Department.Phone]
                FROM 
                    EmployeesDb e
                LEFT JOIN 
                    DepartmentsDb d ON e.DepartmentId = d.Id
                WHERE 
                    e.CompanyId = @CompanyId";

            await EnsureConnectionOpenAsync();

            return await _connection.QueryAsync<Employee>(sql, new { CompanyId = companyId });
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string departmentName)
        {
            var sql = @"
                SELECT 
                    e.Id, 
                    e.Name, 
                    e.Surname, 
                    e.Phone, 
                    e.CompanyId,
                    e.PassportType AS [Passport.Type], 
                    e.PassportNumber AS [Passport.Number],
                    d.Name AS [Department.Name],
                    d.Phone AS [Department.Phone]
                FROM 
                    EmployeesDb e
                JOIN 
                    DepartmentsDb d ON e.DepartmentId = d.Id
                WHERE 
                    d.Name = @DepartmentName";

            await EnsureConnectionOpenAsync();

            return await _connection.QueryAsync<Employee>(sql, new
            {
                DepartmentName = departmentName
            });
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, Employee employee)
        {
            int? departmentId = null;

            if (employee.Department != null)
            {
                departmentId = await _connection.ExecuteScalarAsync<int?>(
                    "SELECT Id FROM DepartmentsDb WHERE Name = @Name AND Phone = @Phone",
                    new
                    {
                        employee.Department.Name,
                        employee.Department.Phone
                    });

                if (departmentId == null)
                {
                    departmentId = await _connection.ExecuteScalarAsync<int>(
                        @"INSERT INTO DepartmentsDb (Name, Phone)
                  VALUES (@Name, @Phone);
                  SELECT CAST(SCOPE_IDENTITY() as int)",
                        new
                        {
                            employee.Department.Name,
                            employee.Department.Phone
                        });
                }
            }

            var sql = @"
                UPDATE 
                    EmployeesDb
                SET 
                    Name = ISNULL(@Name, Name),
                    Surname = ISNULL(@Surname, Surname),
                    Phone = ISNULL(@Phone, Phone),
                    CompanyId = ISNULL(@CompanyId, CompanyId),
                    PassportType = ISNULL(@PassportType, PassportType),
                    PassportNumber = ISNULL(@PassportNumber, PassportNumber),
                    DepartmentId = ISNULL(@DepartmentId, DepartmentId)
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
                DepartmentId = departmentId
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