using FluentValidation;
using School.Application.DTOs;

namespace School.Application.Validators.Authentication
{
    public interface IValidationService
    {
        Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator);
    }
}
