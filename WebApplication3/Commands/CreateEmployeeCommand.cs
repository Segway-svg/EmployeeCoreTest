using FluentValidation.Results;
using System.Net;
using WebApplication3.Mappers.CreateMapper;
using WebApplication3.Models.Response;
using WebApplication3.Repositories;
using WebApplication3.Validators;

namespace WebApplication3.Commands
{
    public class CreateEmployeeCommand : ICreateEmployeeCommand
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDbCreateEmployeeMapper _mapper;
        private readonly IEmployeeRepository _repository;
        private readonly ICreateEmployeeValidator _validator;

        public CreateEmployeeCommand(
          IHttpContextAccessor contextAccessor,
          IDbCreateEmployeeMapper mapper,
          IEmployeeRepository repository,
          ICreateEmployeeValidator validator
            )
        {
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        public async Task<OperationResultResponse<int>> ExecuteAsync(CreateEmployeeRequest request)
        {
            OperationResultResponse<int> response = new();

            ValidationResult validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var result = new OperationResultResponse<int>
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors
                        .Select(e => new ResponseError
                        {
                            Code = (int)HttpStatusCode.BadRequest,
                            Messages = e.ErrorMessage
                        })
                        .ToList()
                };
                
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }

            var employeeId = await _repository.AddEmployeeAsync(_mapper.Map(request));

            if (employeeId != null)
            {
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

                response.IsSuccess = true;
                response.Body = (int)employeeId;
            }
            else
            {
                response.IsSuccess = false;
                
                response.Errors.Add(new ResponseError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Messages = "Ошибка добавления элемента",
                });

                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            }

            return response;
        }
    }
}