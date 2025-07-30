using System.Net;
using WebApplication3.Commands.Interfaces;
using WebApplication3.Mappers.CreateMapper;
using WebApplication3.Mappers.EditMapper;
using WebApplication3.Models.Response;
using WebApplication3.Repositories;
using WebApplication3.Requests;

namespace WebApplication3.Commands
{
    public class EditEmployeeCommand : IEditEmployeeCommand
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDbEditEmployeeMapper _mapper;
        private readonly IEmployeeRepository _repository;

        public EditEmployeeCommand(
          IHttpContextAccessor contextAccessor,
          IDbEditEmployeeMapper mapper,
          IEmployeeRepository repository
        )
        {
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<OperationResultResponse<Employee>> ExecuteAsync(int id, EditEmployeeRequest request)
        {
            OperationResultResponse<Employee> response = new();

            var updatedEmployee = await _repository.UpdateEmployeeAsync(id, _mapper.Map(request));

            if (updatedEmployee != null)
            {
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

                response.IsSuccess = true;
                response.Body = updatedEmployee;
            }
            else
            {
                response.IsSuccess = false;

                response.Errors.Add(new ResponseError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Messages = "Ошибка редактирования",
                });

                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            }

            return response;
        }
    }
}
