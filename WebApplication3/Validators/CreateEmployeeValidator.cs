using FluentValidation;

namespace WebApplication3.Validators
{
    public class CreateEmployeeRequestValidator
        : AbstractValidator<CreateEmployeeRequest>,
          ICreateEmployeeValidator
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(50).WithMessage("Имя слишком длинное");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Фамилия обязательна")
                .MaximumLength(50).WithMessage("Фамилия слишком длинная");

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9]{10,15}$")
                .WithMessage("Некорректный формат телефона");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("Неверный ID компании");

            RuleFor(x => x.Passport)
                .NotNull().WithMessage("Паспортные данные обязательны");

            RuleFor(x => x.Department)
                .NotNull().WithMessage("Данные отдела обязательны");
        }
    }
}