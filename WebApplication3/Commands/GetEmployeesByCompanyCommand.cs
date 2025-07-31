using System.Net;
using WebApplication3.Commands.Interfaces;
using WebApplication3.Mappers;
using WebApplication3.Repositories;
using WebApplication3.Responses;
using WebApplication3.Validators;

namespace WebApplication3.Commands
{
    public class GetEmployeesByCompanyCommand : IGetEmployeesByCompanyCommand
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmployeeRepository _repository;
        private readonly IDtoEmployeeMapper _mapperDto;

        public GetEmployeesByCompanyCommand(
          IHttpContextAccessor contextAccessor,
          IDtoEmployeeMapper mapperDto,
          IEmployeeRepository repository
            )
        {
            _contextAccessor = contextAccessor;
            _mapperDto = mapperDto;
            _repository = repository;
        }
        public async Task<OperationResultResponse<IEnumerable<EmployeeResponse>>> ExecuteAsync(int companyId)
        {
            var employees = await _repository.GetEmployeesByCompanyAsync(companyId);

            if (employees == null || !employees.Any())
            {
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                return new OperationResultResponse<IEnumerable<EmployeeResponse>>
                {
                    IsSuccess = false,
                    Errors = new List<ResponseError>
                    {
                        new ResponseError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            Messages = "Сотрудники для указанной компании не найдены",
                        }
                    }
                };
            }

            var employeesResponse = employees.Select(_mapperDto.Map).ToList();

            _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;

            return new OperationResultResponse<IEnumerable<EmployeeResponse>>
            {
                IsSuccess = true,
                Body = employeesResponse
            };
        }
    }
}
