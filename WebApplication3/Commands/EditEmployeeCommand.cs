using System.Net;
using WebApplication3.Commands.Interfaces;
using WebApplication3.Mappers;
using WebApplication3.Repositories;
using WebApplication3.Requests;
using WebApplication3.Responses;
using WebApplication3.Validators.Interfaces;

namespace WebApplication3.Commands
{
    public class EditEmployeeCommand : IEditEmployeeCommand
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEditEmployeeValidator _validator;
        private readonly IEmployeeRepository _repository;

        public EditEmployeeCommand(
            IHttpContextAccessor contextAccessor,
            IEditEmployeeValidator validator,
            IEmployeeRepository repository)
        {
            _contextAccessor = contextAccessor;
            _validator = validator;
            _repository = repository;
        }

        public async Task<OperationResultResponse<bool>> ExecuteAsync(int id, EditEmployeeRequest request)
        {
            var response = new OperationResultResponse<bool>();

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;

                response.Errors = validationResult.Errors
                    .Select(error => new ResponseError
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Messages = error.ErrorMessage
                    })
                    .ToList();
                
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                
                return response;
            }

            var employeeToUpdate = new Employee { Id = id };

            if (request.Name != null)
                employeeToUpdate.Name = request.Name;

            if (request.Surname != null)
                employeeToUpdate.Surname = request.Surname;

            if (request.Phone != null)
                employeeToUpdate.Phone = request.Phone;

            if (request.CompanyId != null)
                employeeToUpdate.CompanyId = request.CompanyId.Value;

            if (request.Passport != null)
                employeeToUpdate.Passport = request.Passport;

            if (request.Department != null)
                employeeToUpdate.Department = request.Department;

            var updatedEmployee = await _repository.UpdateEmployeeAsync(id, employeeToUpdate);

            if (updatedEmployee != null)
            {
                response.IsSuccess = true;
                response.Body = true;
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                response.IsSuccess = false;
                response.Errors.Add(new ResponseError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Messages = "Ошибка редактирования"
                });
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return response;
        }
    }
}