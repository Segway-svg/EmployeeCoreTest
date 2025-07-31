using Azure.Core;
using System.Net;
using WebApplication3.Commands.Interfaces;
using WebApplication3.Mappers.CreateMapper;
using WebApplication3.Models.Response;
using WebApplication3.Repositories;
using WebApplication3.Validators;

namespace WebApplication3.Commands
{
    public class DeleteEmployeeCommand : IDeleteEmployeeCommand
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmployeeRepository _repository;

        public DeleteEmployeeCommand(
          IHttpContextAccessor contextAccessor,
          IEmployeeRepository repository
            )
        {
            _contextAccessor = contextAccessor;
            _repository = repository;
        }

        public async Task<OperationResultResponse<bool>> ExecuteAsync(int id)
        {
            OperationResultResponse<bool> response = new();

            var isEmployeeExist = await _repository.DeleteEmployeeAsync(id);

            if (isEmployeeExist)
            {
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Accepted;

                response.IsSuccess = true;
                response.Body = true;
            }
            else
            {
                response.IsSuccess = false;

                response.Errors.Add(new ResponseError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Messages = "Не существует сотрудника с таким id",
                });

                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            return response;
        }
    }
}
