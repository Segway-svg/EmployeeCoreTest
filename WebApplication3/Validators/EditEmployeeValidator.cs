using FluentValidation;
using WebApplication3.Data;
using WebApplication3.Requests;
using WebApplication3.Validators.Interfaces;

namespace WebApplication3.Validators
{
    public class EditEmployeeValidator
        : AbstractValidator<EditEmployeeRequest>,
          IEditEmployeeValidator
    {
        public EditEmployeeValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Имя слишком длинное")
                .When(x => x.Name != null);

            RuleFor(x => x.Surname)
                .MaximumLength(50).WithMessage("Фамилия слишком длинная")
                .When(x => x.Surname != null);

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9]{10,15}$")
                .WithMessage("Некорректный формат телефона сотрудника")
                .When(x => x.Phone != null);

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("Неверный Id компании")
                .When(x => x.CompanyId.HasValue);

            RuleFor(x => x.Passport)
                .SetValidator(new PassportValidator())
                .When(x => x.Passport != null);

            RuleFor(x => x.Department)
                .SetValidator(new DepartmentValidator())
                .When(x => x.Department != null);
        }
    }

    public class PassportValidator : AbstractValidator<Passport>
    {
        public PassportValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Тип паспорта обязателен")
                .MaximumLength(20).WithMessage("Тип паспорта слишком длинный");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Номер паспорта обязателен")
                .MaximumLength(20).WithMessage("Номер паспорта слишком длинный");
        }
    }

    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название отдела обязательно")
                .MaximumLength(100).WithMessage("Название отдела слишком длинное");

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9]{10,15}$")
                .WithMessage("Некорректный формат телефона отдела")
                .When(x => x.Phone != null); // Если телефон отдела передан
        }
    }
}