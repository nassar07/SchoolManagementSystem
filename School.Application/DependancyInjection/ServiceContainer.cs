using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using School.Application.Services.Implementation;
using School.Application.Services.Implementation.Authentication;
using School.Application.Services.Interfaces;
using School.Application.Services.Interfaces.Authentication;
using School.Application.Validators.Authentication;

namespace School.Application.DependancyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<ISubmissionsService, SubmissionsService>();


            return services;

        }

    }
}
