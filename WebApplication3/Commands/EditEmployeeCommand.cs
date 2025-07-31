using System.Net;
using WebApplication3.Commands.Interfaces;
using WebApplication3.Mappers;
using WebApplication3.Mappers.EditMapper;
using WebApplication3.Repositories;
using WebApplication3.Requests;
using WebApplication3.Responses;

namespace WebApplication3.Commands
{
    public class EditEmployeeCommand : IEditEmployeeCommand
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDbEditEmployeeMapper _mapper;
        private readonly IDtoEmployeeMapper _mapperDto;
        private readonly IEmployeeRepository _repository;

        public EditEmployeeCommand(
          IHttpContextAccessor contextAccessor,
          IDbEditEmployeeMapper mapper,
          IDtoEmployeeMapper mapperDto,
          IEmployeeRepository repository
        )
        {
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _mapperDto = mapperDto;
            _repository = repository;
        }

        public async Task<OperationResultResponse<EmployeeResponse>> ExecuteAsync(int id, EditEmployeeRequest request)
        {
            OperationResultResponse<EmployeeResponse> response = new();

            var updatedEmployee = await _repository.UpdateEmployeeAsync(id, _mapper.Map(request));

            if (updatedEmployee != null)
            {
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

                response.IsSuccess = true;
                response.Body = _mapperDto.Map(updatedEmployee);
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
