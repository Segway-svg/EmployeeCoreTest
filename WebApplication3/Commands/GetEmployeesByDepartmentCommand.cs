using System.Net;
using WebApplication3.Commands.Interfaces;
using WebApplication3.Mappers;
using WebApplication3.Repositories;
using WebApplication3.Responses;

namespace WebApplication3.Commands
{
    public class GetEmployeesByDepartmentCommand : IGetEmployeesByDepartmentCommand
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmployeeRepository _repository;
        private readonly IDtoEmployeeMapper _mapperDto;

        public GetEmployeesByDepartmentCommand(
          IHttpContextAccessor contextAccessor,
          IDtoEmployeeMapper mapperDto,
          IEmployeeRepository repository
            )
        {
            _contextAccessor = contextAccessor;
            _mapperDto = mapperDto;
            _repository = repository;
        }
        public async Task<OperationResultResponse<List<EmployeeResponse>>> ExecuteAsync(string departmentName)
        {
            var employees = await _repository.GetEmployeesByDepartmentAsync(departmentName);

            if (employees == null || !employees.Any())
            {
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                return new OperationResultResponse<List<EmployeeResponse>>
                {
                    IsSuccess = false,
                    Errors = new List<ResponseError>
                    {
                        new ResponseError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            Messages = "Сотрудники для указанного департамента не найдены",
                        }
                    }
                };
            }

            var employeesResponse = employees.Select(_mapperDto.Map).ToList();

            _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;

            return new OperationResultResponse<List<EmployeeResponse>>
            {
                IsSuccess = true,
                Body = employeesResponse
            };
        }
    }
}
