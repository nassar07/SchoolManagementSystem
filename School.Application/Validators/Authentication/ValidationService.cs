using FluentValidation;
using School.Application.DTOs;

namespace School.Application.Validators.Authentication
{
    public class ValidationService : IValidationService
    {
        public async Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator)
        {
            var _validation = await validator.ValidateAsync(model);
            if (!_validation.IsValid)
            {
                var errors = _validation.Errors.Select(e => e.ErrorMessage).ToList();
                var ErrorsToString = string.Join("; ", errors);
                return new ServiceResponse { Message = ErrorsToString };
            }
            return new ServiceResponse { Success = true };
        }
    }
}
