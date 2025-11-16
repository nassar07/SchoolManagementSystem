using FluentValidation;
using School.Application.DTOs.Department;

namespace School.Application.Validators.Department
{
    public class CreateDepartmentValidation : AbstractValidator<CreateDepartmentDto>
    {
        public CreateDepartmentValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required.");
        }
    }
}
